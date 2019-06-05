using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;

namespace loginreg.Models.Data
{
	public class User
	{
		[Key]
		public int UserID {get;set;}
		public string FirstName {get;set;}
		public string LastName {get;set;}
		public string Email {get;set;}
		public string Password {get;set;}
		public DateTime CreatedAt {get;set;}
		public DateTime UpdatedAt{get;set;}
	}
}