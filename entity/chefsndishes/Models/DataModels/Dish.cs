using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace chefsndishes.Models
{
	public class Dish
	{
		[Key]
		public int DishID {get;set;}

		public string Name {get;set;}

		public int Calories {get;set;}

		public int Tastiness {get;set;}

		public string Description {get;set;}

		public int ChefID {get;set;}
		public Chef Creator {get;set;}

		[Timestamp]
		public DateTime CreatedAt {get;set;}

		[Timestamp]
		public DateTime UpdatedAt {get;set;}
	}
}