using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crudelicious.Models;

namespace crudelicious.Controllers
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
				List<Dish> AllDishes = dbContext.Dishes.ToList();
				return View(AllDishes);
			}

			[HttpGet("new")]
			public IActionResult AddForm()
			{
				return View();
			}

			[HttpPost("new")]
			public IActionResult AddDish(Dish newDish)
		{
			if (newDish.DishID == 0)
			{
				if (ModelState.IsValid)
				{
					dbContext.Dishes.Add(newDish);
					dbContext.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					return View("AddForm");
				}
			}
			return RedirectToAction("UpdateDish", newDish);
		}

		[HttpGet("{id}")]
			
			public IActionResult DishDetails(int id)
			{
				Dish dish = dbContext.Dishes.Single(d => d.DishID == id);
				return View(dish);
			}

			[HttpGet("edit/{id}")]
			public IActionResult EditDishForm(int id)
			{
				Dish dish = dbContext.Dishes.Single(d => d.DishID == id);
				return View("AddForm", dish);
			}

			[HttpPost("edit/{id}")]
			public IActionResult UpdateDish(Dish dish)
			{
					Dish thisDish = dbContext.Dishes.Single(d => d.DishID == dish.DishID);
					thisDish.Name = dish.Name;
					thisDish.Chef = dish.Chef;
					thisDish.Tastiness = dish.Tastiness;
					thisDish.Calories = dish.Calories;
					thisDish.Description = dish.Description;
					return RedirectToAction("Index");
			}

			[HttpGet("/delete/{id}")]
			public IActionResult Delete(int id)
			{
				Dish thisDish = dbContext.Dishes.Single(d => d.DishID == id);
				dbContext.Dishes.Remove(thisDish);
				dbContext.SaveChanges();
				return RedirectToAction("Index");
			}
    }
}
