using System.Collections.Generic;

namespace products.Models
{
  public class CategoryDisplay
  {
    public string Name {get;set;}
    public List<Product> Members {get;set;}
    public AddProduct AddProductModel {get;set;}
  }
}