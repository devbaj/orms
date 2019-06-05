using Microsoft.EntityFrameworkCore;

namespace follow.Models
{
	public class Context : DbContext
	{
		public Context(DbContextOptions options) : base(options) {}
		public DbSet<User> Users {get;set;}
	}
}
