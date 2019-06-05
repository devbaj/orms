using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;

namespace loginreg.Models.Forms
{
  public class Register
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
    // ? Custom Validation?
    public string Email {get;set;}

    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password {get;set;}

    [Compare("Password")]
    [Display(Name = "Re-type Password")]
    [DataType(DataType.Password)]
    public string PasswordConfirmation{get;set;}
  }
}