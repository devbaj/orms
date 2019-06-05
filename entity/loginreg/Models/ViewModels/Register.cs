using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;

namespace loginreg.Models.Views
{
	public class Register
	{
		public string FirstName {get;set;}
		public string LastName {get;set;}
		public string Email {get;set;}
		public string Password {get;set;}
		public string PasswordConfirmation{get;set;}
	}
}