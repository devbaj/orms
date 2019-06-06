using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace chefsndishes.Models
{
	public class Chef
	{
		[Key]
		public int ChefID {get;set;}
		public string FirstName {get;set;}
		public string LastName {get;set;}
		public DateTime BirthDate {get;set;}
		public List<Dish> CreatedDishes {get;set;}
		public DateTime CreatedAt {get;set;}
		public DateTime UpdatedAt {get;set;}
	}
}