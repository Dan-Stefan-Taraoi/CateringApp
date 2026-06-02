using CateringApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
                
        }

        public DbSet<Item> Items { get; set; }
    }
}
