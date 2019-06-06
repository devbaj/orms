using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bank.Models;

namespace bank.Controllers
{
    public class HomeController : Controller
    {
        private Context dbContext;

        public HomeController(Context context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult UserToDB(RegisterForm regData)
        {

            if ( dbContext.Users.Any(u => u.Email == regData.Email))
                ModelState.AddModelError("Email", "Email already in use");
            if (!ModelState.IsValid)
                return View("Index");

            User newUser = new User();
            newUser.FirstName = regData.FirstName;
            newUser.LastName = regData.LastName;
            newUser.Email = regData.Email;
            var Hasher = new PasswordHasher<RegisterForm>();
            newUser.Password = Hasher.HashPassword(regData, regData.Password);
            newUser.Balance = 0;
            dbContext.Add(newUser);
            dbContext.SaveChanges();

            return RedirectToAction("LogInUser");
        }

        [HttpPost("login/process")]
        public IActionResult LogInUser(LoginForm loginData)
        {
            var match = dbContext.Users
                .FirstOrDefault(u => u.Email == loginData.Email);
            if (match == null)
                ModelState.AddModelError("Email", "Invalid email or password");
            var Hasher = new PasswordHasher<LoginForm>();
            var result = Hasher
                .VerifyHashedPassword(
                    loginData, match.Password, loginData.Password
                );
            if (result == 0)
                ModelState.AddModelError("Email", "Invalid email or password");
            if (!ModelState.IsValid)
                return View("Login");
            
            int userID = match.UserID;
            HttpContext.Session.SetInt32("user", userID);
            return Redirect($"/account/{userID}");
        }

        [HttpGet("account/{id}")]
        public IActionResult Account(int id)
        {
            if (HttpContext.Session.GetInt32("user") == null)
                return Redirect("/");
            User thisUser = dbContext.Users
                .Include(u => u.Transactions)
                .SingleOrDefault(u => u.UserID == id);
            Account thisAccount = new Account();
            thisAccount.Username = $"{thisUser.FirstName} {thisUser.LastName}";
            thisAccount.Balance = string.Format("{0:C}", thisUser.Balance);

            thisAccount.History = new List<History>();
            foreach (Transaction t in thisUser.Transactions)
            {
                History temp = new History();
                temp.Amount = string.Format("{0:C}", t.Amount);
                temp.Date = t.CreatedAt;
                thisAccount.History.Add(temp);
            }

            return View(thisAccount);
        }

        [HttpPost("transact")]
        public IActionResult Transact(ChangeForm transactionData)
        {
            User thisUser = dbContext.Users
                .Include(u => u.Transactions)
                .FirstOrDefault( u =>
                    u.UserID == HttpContext.Session.GetInt32("user"));
            if (transactionData.Amount == 0)
                ModelState.AddModelError("Amount", "Enter an amount");
            else if (transactionData.Amount < 0)
            {
                if (Math.Abs(transactionData.Amount) > thisUser.Balance)
                    ModelState
                        .AddModelError("Amount", "Insufficient funds");
            }
            if (!ModelState.IsValid)
                return RedirectToAction("Account", new{
                    id = HttpContext.Session.GetInt32("user")
                });

            thisUser.Balance += transactionData.Amount;

            Transaction newTransaction = new Transaction();
            newTransaction.Amount = transactionData.Amount;
            newTransaction.UserID = (int)HttpContext.Session.GetInt32("user");
            newTransaction.CreatedAt = DateTime.Now;
            dbContext.Transactions.Add(newTransaction);
            dbContext.SaveChanges();

            return RedirectToAction("Account", new{
                id = HttpContext.Session.GetInt32("user")
            });
        }
    }
}
