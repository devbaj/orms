using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace products.Models
{
  public class AddProduct
  {
    [Required]
    [Display(Name = "Add Product")]
    public int NewProductID {get;set;}
    public List<Product> OtherProducts {get;set;}
  }
}