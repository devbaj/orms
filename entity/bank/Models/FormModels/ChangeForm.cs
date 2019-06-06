using System;
using System.ComponentModel.DataAnnotations;

namespace bank.Models
{
  public class ChangeForm
  {
    [Required]
    [DataType(DataType.Currency)]
    [Display(Name = "Deposit/Withdraw")]
    public decimal Amount {get;set;}
  }
}