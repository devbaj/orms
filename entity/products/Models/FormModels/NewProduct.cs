using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace products.Models
{
  public class NewProduct
  {
    [Required]
    [MinLength(2)]
    public string Name {get;set;}

    [Required]
    [MinLength(2)]
    public string Description {get;set;}

    [Required]
    [Range(0,9999)]
    public decimal Price {get;set;}
    public List<Product> AllProducts {get;set;}
  }
}