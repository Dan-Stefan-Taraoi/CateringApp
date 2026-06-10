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
            modelBuilder.Entity<Item>().ToTable("Items");

            // ItemClient composite key
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

            // Explicitly configure TPH hierarchy
            modelBuilder.Entity<Item>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<MenuItem>("MenuItem")
                .HasValue<Ingredient>("Ingredient")
                .HasValue<HardwareItem>("HardwareItem");

            // SerialNumber → HardwareItem relationship
            modelBuilder.Entity<HardwareItem>()
                .HasOne(h => h.SerialNumber)
                .WithOne(s => s.HardwareItem)
                .HasForeignKey<SerialNumber>(s => s.HardwareItemId);

            // Seed data
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Ingredients" },
                    new Category { Id = 2, Name = "Dishes" },
                    new Category { Id = 3, Name = "Hardware" }
                );

            base.OnModelCreating(modelBuilder);
        }

        // Item hierarchy — Item is abstract, no DbSet<Item> needed
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<HardwareItem> HardwareItems { get; set; }

        public DbSet<InventoryItem> InventoryItems { get; set; }

        // Supporting entities
        public DbSet<SerialNumber> SerialNumbers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ItemClient> ItemClients { get; set; }
    }
}