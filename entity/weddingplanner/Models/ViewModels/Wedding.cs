using System;
using System.Collections.Generic;

namespace weddingplanner.Models
{
  public class Wedding
  {
    public string Couple {get;set;}
    public DateTime Date {get;set;}
    public string Address {get;set;}
    public List<User> Guests {get;set;}
  }
}