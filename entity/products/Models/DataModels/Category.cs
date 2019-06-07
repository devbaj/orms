using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace products.Models
{
  public class Category
  {
    [Key]
    public int CategoryID {get;set;}
    public string Name {get;set;}
    public List<Association> Products {get;set;}
    public DateTime CreatedAt {get;set;}
    public DateTime UpdatedAt {get;set;}
  }
}