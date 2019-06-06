using System.ComponentModel.DataAnnotations;
using System;

namespace chefsndishes.Models
{
	public class NewChef
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
		[DataType(DataType.Date)]
		[Display(Name = "Date of Birth")]
		public DateTime BirthDate {get;set;}
	}
}