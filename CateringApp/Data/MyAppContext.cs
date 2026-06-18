using CateringApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Data
{
    public class MyAppContext : IdentityDbContext<IdentityUser>
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Items");
            modelBuilder.Entity<Order>().ToTable("Orders");

            // Item-to-MenuItem: KitchenItem - composite key
            modelBuilder.Entity<KitchenItem>()
                .HasKey(ki => new { ki.ItemId, ki.MenuItemId });

            modelBuilder.Entity<KitchenItem>()
                .HasOne(ki => ki.Item)
                .WithMany(i => i.KitchenItems)
                .HasForeignKey(ki => ki.ItemId);

            modelBuilder.Entity<KitchenItem>()
                .HasOne(ki => ki.MenuItem)
                .WithMany(c => c.KitchenItems)
                .HasForeignKey(ki => ki.MenuItemId);

            // Order-to-MenuItem : MenuOrderEntry relationships — Id is PK, no composite key
            modelBuilder.Entity<MenuOrderEntry>()
                .HasOne(moe => moe.Order)
                .WithMany(o => o.Entries)
                .HasForeignKey(moe => moe.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // cascade delete

            modelBuilder.Entity<MenuOrderEntry>()
                .HasOne(moe => moe.MenuItem)
                .WithMany(mi => mi.MenuOrderEntries)
                .HasForeignKey(moe => moe.MenuItemId);

            // Seed data
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Name = "Ingredients" },
                    new Category { Id = 2, Name = "Equipment" },
                    new Category { Id = 3, Name = "Maintenance" },
                    new Category { Id = 4, Name = "Operational" }
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Item> Items { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<KitchenItem> KitchenItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<MenuOrderEntry> MenuOrderEntries { get; set; }
    }
}