using Microsoft.EntityFrameworkCore;
using chefsndishes.Models;

namespace chefsndishes.Models
{
	public class Context : DbContext
	{
		public Context(DbContextOptions options) : base(options){}
		public DbSet<Dish> Dishes {get;set;}
		public DbSet<Chef> Chefs {get;set;}
	}
}