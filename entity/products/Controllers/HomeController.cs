using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using products.Models;
using Microsoft.EntityFrameworkCore;

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
            List<Product> AllProducts = dbContext.Products.ToList();
            NewProduct emptyForm = new NewProduct();
            emptyForm.AllProducts = AllProducts;
            return View("NewProduct", emptyForm);
        }

        [HttpGet("categories")]
        public IActionResult Categories()
        {
            List<Category> AllCategories = dbContext.Categories.ToList();
            NewCategory emptyForm  = new NewCategory();
            emptyForm.AllCategories = AllCategories;
            return View("NewCategory", emptyForm);
        }

        [HttpPost("products")]
        public IActionResult ProductToDB(NewProduct formData)
        {
            if (!ModelState.IsValid)
                return View("NewProduct");

            Product newProduct = new Product();
            newProduct.Name = formData.Name;
            newProduct.Description = formData.Description;
            newProduct.Price = formData.Price;
            newProduct.CreatedAt = DateTime.Now;
            newProduct.UpdatedAt = DateTime.Now;
            dbContext.Products.Add(newProduct);
            dbContext.SaveChanges();

            return RedirectToAction("Products");
        }

        [HttpPost("categories")]
        public IActionResult CategoryToDB(NewCategory formData)
        {
            if (!ModelState.IsValid)
                return View("NewCategory");

            Category newCategory = new Category();
            newCategory.Name = formData.Name;
            newCategory.CreatedAt = DateTime.Now;
            newCategory.UpdatedAt = DateTime.Now;
            dbContext.Categories.Add(newCategory);
            dbContext.SaveChanges();

            return RedirectToAction("Categories");
        }

        [HttpGet("products/{id}")]
        public IActionResult DisplayProduct(int id)
        {
            Product selectedProduct = dbContext.Products
                .Include(p => p.Categories)
                .ThenInclude(asc => asc.Category)
                .FirstOrDefault(p => p.ProductID == id);
            if (selectedProduct == null)
                return RedirectToAction("Products");

            ProductDisplay thisProduct = new ProductDisplay();
            thisProduct.Name = selectedProduct.Name;

            List<Category> AllCategories = dbContext.Categories.ToList();
            thisProduct.AddCategoryModel = new AddCategory();
            thisProduct.AddCategoryModel.OtherCategories = new List<Category>();
            bool found = false;
            foreach (Category c in AllCategories)
            {
                foreach (Association a in selectedProduct.Categories)
                {
                    if (a.Category == c)
                    {
                        found = true;
                        thisProduct.BelongsTo.Add(c);
                    }
                }
                if (!found)
                {
                    thisProduct.AddCategoryModel.OtherCategories.Add(c);
                }
            }


            // todo pass list of categories not assigned to this product

            return View("ProductDisplay", thisProduct);
        }

        [HttpGet("categories/{id}")]
        public IActionResult DisplayCategory(int id)
        {
            Category selectedCategory = dbContext.Categories
                .Include(c => c.Products)
                .ThenInclude(asc => asc.Product)
                .FirstOrDefault(c => c.CategoryID == id);
            if (selectedCategory == null)
                return RedirectToAction("Categories");

            CategoryDisplay thisCategory = new CategoryDisplay();
            thisCategory.Name = selectedCategory.Name;
            foreach (Association a in selectedCategory.Products)
            {
                thisCategory.Members.Add(a.Product);
            }
            thisCategory.AddProductModel = new AddProduct();

            return View("CategoryDisplay", thisCategory);
        }

        [HttpPost("categories/{id}/addproduct")]
        public IActionResult AddProductToCategory(int id, AddProduct formData)
        {
            Category thisCategory = dbContext.Categories
                .FirstOrDefault(c => c.CategoryID == id);
            if (thisCategory == null)
                ModelState.AddModelError("ProductID", "No Product Selected");
            if (!ModelState.IsValid)
                return Redirect($"categories/{id}");
            
            Product addedProduct = dbContext.Products
                .FirstOrDefault(p => p.ProductID == formData.NewProductID);

            // todo adds posted product to this category(id param)
            return RedirectToAction("");
        }

        [HttpPost("products/{id}/addcategory")]
        public IActionResult AddCategoryToPRoduct(int id, AddCategory formData)
        {
            // todo adds posted category to this product(id)
            return RedirectToAction("");
        }
    }
}
