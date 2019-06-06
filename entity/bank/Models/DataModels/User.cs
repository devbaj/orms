using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bank.Models
{
  public class User
  {
    [Key]
    public int UserID {get;set;}
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get;set;}
    public string Password {get;set;}
    public decimal Balance {get;set;}
    public List<Transaction> Transactions {get;set;}
  }
}