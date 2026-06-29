using CateringApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringApp.Tests.Models
{
    public class OrderTests
    {
        private readonly Client _client = new() { Id = 2, Name = "Firma Mueller GmbH" };

        private List<MenuItem> BuildMenuItems() =>
        [
            new MenuItem { Id = 1, Name = "Pizza", Price = 12.00 },
        new MenuItem { Id = 2, Name = "Lasagne", Price = 14.00 }
        ];

        [Fact]
        public void Create_Restaurant_SetsCorrectFlags()
        {
            var order = Order.Create(_client, "Restaurant", BuildMenuItems());

            Assert.False(order.RequiresTransport);
            Assert.False(order.IsBulkPackaged);
            Assert.Equal("Restaurant", order.ServiceType);
        }

        [Fact]
        public void Create_Catering_SetsCorrectFlags()
        {
            var order = Order.Create(_client, "Catering", BuildMenuItems(), "Messe Nürnberg");

            Assert.True(order.RequiresTransport);
            Assert.True(order.IsBulkPackaged);
            Assert.Equal("Messe Nürnberg", order.Location);
        }

        [Fact]
        public void Create_SnapshotsMenuItemNameAndPrice()
        {
            var order = Order.Create(_client, "Restaurant", BuildMenuItems());

            Assert.Equal("Pizza", order.Entries[0].MenuItemName);
            Assert.Equal(12.00, order.Entries[0].UnitPrice);
        }

        [Fact]
        public void Create_SetsCreatedAtToNow()
        {
            var before = DateTime.UtcNow;
            var order = Order.Create(_client, "Restaurant", BuildMenuItems());
            var after = DateTime.UtcNow;

            Assert.InRange(order.CreatedAt, before, after);
        }

        [Fact]
        public void Total_CalculatesCorrectly()
        {
            var order = Order.Create(_client, "Restaurant", BuildMenuItems());
            // 2 entries, quantity 1 each: 12 + 14 = 26
            Assert.Equal(26.00, order.Total);
        }
    }
}
