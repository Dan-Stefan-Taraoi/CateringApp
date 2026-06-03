using CateringApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasData(
                    new Item { Id = 10, Name = "Pizza", Description = "Delicious cheese pizza", Price = 9.99, SerialNumberId = 1 }
                );

            modelBuilder.Entity<SerialNumber>()
                .HasData(
                    new SerialNumber { Id = 1, Name = "PizzaHUBS_09", ItemId = 10 }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<SerialNumber> SerialNumbers { get; set; }
    }
}
