using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace products.Models
{
  public class NewCategory
  {
    [Required]
    [MinLength(3)]
    public string Name {get;set;}
    public List<Category> AllCategories {get;set;}
  }
}