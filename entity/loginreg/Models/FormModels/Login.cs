using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;

namespace loginreg.Models.Forms
{
	public class Login
	{
		public string Email {get;set;}

		[DataType(DataType.Password)]
		public string Password {get;set;}
	}
}