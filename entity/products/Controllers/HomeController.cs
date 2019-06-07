using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using products.Models;

namespace products.Controllers
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
            return Redirect("products");
        }

        [HttpGet("products")]
        public IActionResult Products()
        {
            // TODO route shows form to add new product and list of existing products
            return View("NewProduct");
        }

        [HttpGet("categories")]
        public IActionResult Categories()
        {
            // TODO route shows form to add new category and list of existing categories
            return View("NewCategory");
        }

        [HttpPost("products")]
        public IActionResult ProductToDB(AddProduct formData)
        {
            return RedirectToAction("Products");
        }

        [HttpPost("categories")]
        public IActionResult CategoryToDB(AddCategory categoryData)
        {
            return RedirectToAction("Categories");
        }

        [HttpGet("products/{id}")]
        public IActionResult ProductDisplay(int id)
        {
            return View("ProductDisplay");
        }
    }
}
