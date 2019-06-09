using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weddingplanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace weddingplanner.Controllers
{
  public class UserController : Controller
  {
    private Context dbContext;
    public UserController(Context context)
    {
      dbContext = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost("register")]
    public IActionResult Register(Register formData)
    {
      if (dbContext.Users.Any(u => u.Email == formData.Email))
        ModelState.AddModelError("Email", "Email already in use");
      if (!ModelState.IsValid)
        return View("Index");

      User newUser = new User();
      newUser.FirstName = formData.FirstName;
      newUser.LastName = formData.LastName;
      newUser.Email = formData.Email;
      var Hasher = new PasswordHasher<Register>();
      newUser.HashedPassword = Hasher.HashPassword(formData, formData.Password);
      newUser.CreatedAt = DateTime.Now;
      newUser.UpdatedAt = DateTime.Now;
      dbContext.Users.Add(newUser);
      dbContext.SaveChanges();

      int userid = dbContext.Users
        .SingleOrDefault(u => u.Email == formData.Email)
        .UserID;

      return RedirectToAction("UserApproved", userid);
    }

    [HttpPost("login")]
    public IActionResult Login(Login formData)
    {
      User thisUser = dbContext.Users
        .SingleOrDefault(u => u.Email == formData.Email);
      if (thisUser == null)
        ModelState.AddModelError("Email", "Invalid Email or Password");
      var Hasher = new PasswordHasher<Login>();
      var match = Hasher.VerifyHashedPassword(
        formData, thisUser.HashedPassword, formData.Password);
      if (match == 0)
        ModelState.AddModelError("Email", "Invalid Email or Password");
      
      int userid = thisUser.UserID;

      return RedirectToAction("UserApproved", userid);
    }

    public IActionResult UserApproved(int userid)
    {
      HttpContext.Session.SetInt32("user", userid);
      return RedirectToAction("Dashboard", "Home");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return Redirect("/");
    }
  }
}