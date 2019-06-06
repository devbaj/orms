using System.ComponentModel.DataAnnotations;
using System;

namespace bank.Models
{
  public class Transaction
  {
    public int TransactionID {get;set;}
    public decimal Amount {get;set;}
    public DateTime CreatedAt {get;set;}
    public int UserID {get;set;}
  }
}