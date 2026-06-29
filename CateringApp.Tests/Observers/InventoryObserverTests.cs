using CateringApp.Data;
using CateringApp.Models;
using CateringApp.Models.Observers;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Tests.Observers
{
    public class InventoryObserverTests
    {
        private MyAppContext BuildContext()
        {
            var options = new DbContextOptionsBuilder<MyAppContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique DB per test
                .Options;
            return new MyAppContext(options);
        }

        [Fact]
        public async Task OnOrderPlaced_DeductsInventoryQuantity()
        {
            var context = BuildContext();

            // Seed an item
            var item = new Item { Id = 1, Name = "Flour", Quantity = 10.0, IsAvailable = true };
            context.Items.Add(item);
            await context.SaveChangesAsync();

            // Build a dish with that kitchen item
            var kitchenItem = new KitchenItem { ItemId = 1, Item = item, QuantityRequired = 2.0 };
            var dish = new RestaurantDish();
            dish.AddKitchenItem(kitchenItem);

            var orderDetails = new OrderDetails
            {
                Dishes = [dish],
                Client = new Client { Id = 1, Name = "Table_05" },
                ClientId = 1
            };

            var observer = new InventoryObserver(context);
            await observer.OnOrderPlacedAsync(new OrderPlacedEvent(orderDetails));

            var updatedItem = await context.Items.FindAsync(1);
            Assert.Equal(8.0, updatedItem!.Quantity); // 10 - 2 = 8
        }

        [Fact]
        public async Task OnOrderPlaced_SetsIsAvailableFalse_WhenStockHitsZero()
        {
            var context = BuildContext();

            var item = new Item { Id = 1, Name = "Flour", Quantity = 1.0, IsAvailable = true };
            context.Items.Add(item);
            await context.SaveChangesAsync();

            var kitchenItem = new KitchenItem { ItemId = 1, Item = item, QuantityRequired = 1.0 };
            var dish = new RestaurantDish();
            dish.AddKitchenItem(kitchenItem);

            var orderDetails = new OrderDetails { Dishes = [dish], ClientId = 1, Client = new Client() };

            var observer = new InventoryObserver(context);
            await observer.OnOrderPlacedAsync(new OrderPlacedEvent(orderDetails));

            var updatedItem = await context.Items.FindAsync(1);
            Assert.False(updatedItem!.IsAvailable);
            Assert.Equal(0, updatedItem.Quantity);
        }
    }
}
