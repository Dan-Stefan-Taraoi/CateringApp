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
            modelBuilder.Entity<ItemClient>()
                .HasKey(ic => new { ic.ItemId, ic.ClientId });

            modelBuilder.Entity<ItemClient>()
                .HasOne(ic => ic.Item)
                .WithMany(i => i.ItemClients)
                .HasForeignKey(ic => ic.ItemId);

            modelBuilder.Entity<ItemClient>()
                .HasOne(ic => ic.Client)
                .WithMany(c => c.ItemClients)
                .HasForeignKey(ic => ic.ClientId);

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Ingredients" },
                    new Category { Id = 2, Name = "Dishes" }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<SerialNumber> SerialNumbers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<ItemClient> ItemClients { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
