using Microsoft.EntityFrameworkCore;

namespace products.Models
{
  public class Context : DbContext
  {
    public Context(DbContextOptions options) : base(options) {}
    public DbSet<Product> Products {get;set;}
    public DbSet<Category> Categories {get;set;}
    public DbSet<Association> Associations {get;set;}
  }
}