using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using chefsndishes.Models;
using Microsoft.EntityFrameworkCore;

namespace chefsndishes.Controllers
{
    public class HomeController : Controller
    {
        public Context dbContext;
        public HomeController(Context context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Index> ViewChefs = new List<Index>();
            List<Chef> AllChefs = dbContext.Chefs
                .Include(dish => dish.CreatedDishes)
                .ToList();
            foreach (Chef c in AllChefs)
            {
                Index thisChef = new Index();
                thisChef.Name = $"{c.FirstName} {c.LastName}";
                thisChef.DishCount = c.CreatedDishes.Count;
                thisChef.Age = (int)((DateTime.Today - c.BirthDate)
                    .TotalDays / 356.2425);
                ViewChefs.Add(thisChef);
            }
            return View(ViewChefs);
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dishes> ViewDishes = new List<Dishes>();
            List<Dish> AllDishes = dbContext.Dishes
                .Include(dish => dish.Creator)
                .ToList();
            foreach (Dish d in AllDishes)
            {
                Dishes thisDish = new Dishes();
                thisDish.Name = d.Name;
                thisDish.Chef = $"{d.Creator.FirstName} {d.Creator.LastName}";
                thisDish.Tastiness = d.Tastiness;
                thisDish.Calories = d.Calories;
                ViewDishes.Add(thisDish);
            }
            return View(ViewDishes);
        }

        [HttpGet("new")]
        public IActionResult AddChefForm() { return View("ChefForm"); }

        [HttpGet("dishes/new")]
        public IActionResult AddDishForm()
        {
            List<Chef> AllChefs = new List<Chef>();
            AllChefs = dbContext.Chefs.ToList();
            NewDish thisDish = new NewDish();
            thisDish.Chefs = AllChefs;
            return View("DishForm", thisDish);
        }

        [HttpPost("new")]
        public IActionResult ChefToDB(NewChef newChef)
        {
            if (newChef.BirthDate > DateTime.Today)
            {
                ModelState.AddModelError("BirthDate", "Invalid date");
            }
            if (!ModelState.IsValid)
            {
                return View("ChefForm");
            }
            Chef thisChef = new Chef();
            thisChef.FirstName = newChef.FirstName;
            thisChef.LastName = newChef.LastName;
            thisChef.BirthDate = newChef.BirthDate;
            thisChef.CreatedAt = DateTime.Now;
            thisChef.UpdatedAt = DateTime.Now;
            dbContext.Chefs.Add(thisChef);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("dishes/new")]
        public IActionResult DishToDB(NewDish newDish)
        {
            if (!ModelState.IsValid)
            {
                return View("DishForm");
            }
            Dish thisDish = new Dish();
            thisDish.Name = newDish.Name;
            thisDish.Calories = newDish.Calories;
            thisDish.Tastiness = newDish.Tastiness;
            thisDish.Description = newDish.Description;
            thisDish.ChefID = newDish.ChefID;
            thisDish.CreatedAt = DateTime.Now;
            thisDish.UpdatedAt = DateTime.Now;
            dbContext.Dishes.Add(thisDish);
            dbContext.SaveChanges();
            return RedirectToAction("Dishes");
        }
    }
}
