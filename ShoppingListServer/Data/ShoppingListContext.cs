using Microsoft.EntityFrameworkCore;
using ShoppingListServer.Models;

namespace ShoppingListServer.Data
{
    public class ShoppingListContext : DbContext
    {
        public ShoppingListContext(DbContextOptions<ShoppingListContext> options)
            : base(options)
        {
        }

        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingItem>()
                .HasIndex(s => s.Name)
                .IsUnique(false);
        }
    }
}
