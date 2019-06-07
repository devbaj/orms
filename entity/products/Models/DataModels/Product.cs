using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace products.Models
{
  public class Product
  {
    [Key]
    public int ProductID {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public decimal Price {get;set;}
    public List<Association> Categories {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
  }
}