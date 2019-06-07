using System.Collections.Generic;

namespace products.Models
{
  public class ProductDisplay
  {
    public string Name {get;set;}
    public List<Category> BelongsTo {get;set;}
    public AddCategory AddCategoryModel {get;set;}
  }
}