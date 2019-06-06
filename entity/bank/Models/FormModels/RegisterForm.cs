using System;
using System.ComponentModel.DataAnnotations;

namespace bank.Models
{
  public class RegisterForm
  {
    [Required]
    [MinLength(2)]
    [Display(Name = "First Name")]
    public string FirstName {get;set;}

    [Required]
    [MinLength(2)]
    [Display(Name = "Last Name")]
    public string LastName {get;set;}

    [Required]
    [EmailAddress]
    public string Email {get;set;}

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password {get;set;}

    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    [Display(Name = "Re-type Password")]
    public string ConfirmPassword {get;set;}
  }
}