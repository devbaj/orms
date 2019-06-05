using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using loginreg.Models;
using loginreg.Models.Data;
using loginreg.Models.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace loginreg.Controllers
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

        [HttpPost("login")]
        public IActionResult Login(Models.Forms.Login attempt)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Email == attempt.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Login");
            }
            var Hasher = new PasswordHasher<Login>();
            var result = Hasher.VerifyHashedPassword(attempt, user.Password, attempt.Password);
            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Email/Password");
            }
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            HttpContext.Session.SetInt32("UserID", user.UserID);
            return RedirectToAction("Success");
        }

        [HttpGet("login")]
        public IActionResult LoginForm()
        {
            return View("Login");
        }

        [HttpPost("register")]
        public IActionResult Register(Models.Forms.Register submittedForm)
        {
            if (dbContext.Users.Any(u => u.Email == submittedForm.Email))
            {
                ModelState.AddModelError("Email", "Email is already in use!");
            }
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            PasswordHasher<Register> Hasher = new PasswordHasher<Register>();
            User newUser = new User();
            newUser.FirstName = submittedForm.FirstName;
            newUser.LastName = submittedForm.LastName;
            newUser.Email = submittedForm.Email;
            newUser.Password = Hasher.HashPassword(submittedForm, submittedForm.Password);
            newUser.CreatedAt = DateTime.Now;
            newUser.UpdatedAt = DateTime.Now;
            dbContext.Add(newUser);
            dbContext.SaveChanges();

            User userInfo = dbContext.Users.OrderByDescending(u => u.CreatedAt).FirstOrDefault();
            HttpContext.Session.SetInt32("UserID", userInfo.UserID);
            return RedirectToAction("Success");
        }

        [HttpGet("success")]
        public IActionResult Success(Models.Data.User user)
        {
            if (!HttpContext.Session.Keys.Contains("UserID"))
                return Redirect("/");
            return View();
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
