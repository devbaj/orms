using System.ComponentModel.DataAnnotations;
using System;

namespace crudelicious.Models
{
	public class Dish
	{
		[Key]
		public int DishID {get;set;}

		[Required]
		[Display(Name = "Name of Dish")]
		public string Name {get;set;}

		[Required]
		[Display(Name = "Chef's Name")]
		public string Chef {get;set;}

		[Required]
		[Range(1,5)]
		public int Tastiness {get;set;}

		[Required]
		[Range(0, 9999)]
		[Display(Name = "# of Calories")]
		public int Calories {get;set;}

		[Required]
		public string Description {get;set;}

		public DateTime CreatedAt {get;set;}
		
		public DateTime UpdatedAt {get;set;}
	}
}