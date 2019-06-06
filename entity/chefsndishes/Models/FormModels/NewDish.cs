using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace chefsndishes.Models
{
	public class NewDish
	{
		[Required]
		[MinLength(2)]
		public string Name {get;set;}

		[Required]
		[Range(0,9999)]
		public int Calories {get;set;}

		[Required]
		[MinLength(2)]
		public string Description {get;set;}

		[Required]
		[Range(1,5)]
		public int Tastiness {get;set;}

		[Required]
		[Display(Name = "Chef")]
		public int ChefID {get;set;}
		public List<Chef> Chefs {get;set;}
	}
}
