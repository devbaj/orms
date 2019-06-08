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
            thisProduct.BelongsTo = new List<Category>();

            List<Category> AllCategories = dbContext.Categories.ToList();
            thisProduct.AddCategoryModel = new AddCategory();
            thisProduct.AddCategoryModel.OtherCategories = new List<Category>();
            bool found;
            foreach (Category c in AllCategories)
            {
                found = false;
                foreach (Association a in selectedProduct.Categories)
                {
                    if (a.Category == c)
                    {
                        found = true;
                        thisProduct.BelongsTo.Add(c);
                    }
                }
                if (!found)
                    thisProduct.AddCategoryModel.OtherCategories.Add(c);
            }
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

            List<Product> AllProducts = dbContext.Products.ToList();
            thisCategory.AddProductModel = new AddProduct();
            thisCategory.AddProductModel.OtherProducts = new List<Product>();
            bool found = false;
            foreach (Product p in AllProducts)
            {
                foreach (Association a in selectedCategory.Products)
                {
                    if (a.Product == p)
                    {
                        found = true;
                        thisCategory.Members.Add(p);
                    }
                }
                if (!found)
                    thisCategory.AddProductModel.OtherProducts.Add(p);
            }
            return View("CategoryDisplay", thisCategory);
        }

        [HttpPost("categories/{id}/addproduct")]
        public IActionResult AddProductToCategory(int id, CategoryDisplay formData)
        {
            // ! formdata is not properly getting the product ID from the post
            Category thisCategory = dbContext.Categories
                .FirstOrDefault(c => c.CategoryID == id);
            if (thisCategory == null)
                ModelState.AddModelError("ProductID", "No Product Selected");
            if (!ModelState.IsValid)
                return Redirect($"categories/{id}");
            
            Product addedProduct = dbContext.Products
                .SingleOrDefault(p => p.ProductID == formData.AddProductModel.NewProductID);
            
            Association asc = new Association();

            asc.Category = thisCategory;
            asc.Product = addedProduct;
            thisCategory.Products = new List<Association>();
            thisCategory.Products.Add(asc);
            addedProduct.Categories = new List<Association>();
            addedProduct.Categories.Add(asc);
            dbContext.SaveChanges();
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
