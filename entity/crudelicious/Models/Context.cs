using Microsoft.EntityFrameworkCore;

namespace crudelicious.Models
{
	public class Context : DbContext
	{
		public Context(DbContextOptions options) : base(options) {}
		public DbSet<Dish> Dishes {get;set;}
	}
}