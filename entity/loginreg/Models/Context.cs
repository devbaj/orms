using Microsoft.EntityFrameworkCore;
using loginreg.Models.Data;

namespace loginreg.Models
{
	public class Context : DbContext
	{
		public Context(DbContextOptions options) : base(options) {}
		public DbSet<User> Users {get;set;}
	}
}