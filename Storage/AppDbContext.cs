using Ambev.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.Storage
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
