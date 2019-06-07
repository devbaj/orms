using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace products.Models
{
  public class AddCategory
  {
    [Required]
    [Display(Name = "Add Category")]
    public int NewCategoryID {get;set;}
    public List<Category> OtherCategories {get;set;}
  }
}