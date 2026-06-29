using CateringApp.Models;
using CateringApp.Models.Interfaces;
using CateringApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringApp.Tests.Services
{
    public class DishServiceTests
    {
        private readonly DishService _service;

        // Helper — builds a real resolver with both factories
        public DishServiceTests()
        {
            var factories = new List<IKitchenFactory>
            {
                new RestaurantKitchenFactory(),
                new CateringKitchenFactory()
            };
            var resolver = new KitchenFactoryResolver(factories);
            _service = new DishService(resolver);
        }

        // Helper — builds a MenuItem with optional kitchen items
        private MenuItem BuildMenuItem(
            string name = "Pizza",
            int prepMinutes = 20,
            List<KitchenItem>? kitchenItems = null)
        {
            return new MenuItem
            {
                Id = 1,
                Name = name,
                Price = 12.00,
                PreparationTime = TimeSpan.FromMinutes(prepMinutes),
                KitchenItems = kitchenItems ?? []
            };
        }

        #region CreateDish

        [Fact]
        public void CreateDish_Restaurant_ReturnsRestaurantDish()
        {
            var dish = _service.CreateDish(BuildMenuItem(), "Restaurant");
            Assert.IsType<RestaurantDish>(dish);
        }

        [Fact]
        public void CreateDish_Catering_ReturnsCateringDish()
        {
            var dish = _service.CreateDish(BuildMenuItem(), "Catering");
            Assert.IsType<CateringDish>(dish);
        }

        [Fact]
        public void CreateDish_Catering_SetsLocation()
        {
            var dish = _service.CreateDish(
                BuildMenuItem(),
                "Catering",
                "Messe Nürnberg") as CateringDish;

            Assert.Equal("Messe Nürnberg", dish!.Location);
        }

        [Fact]
        public void CreateDish_NullMenuItem_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => _service.CreateDish(null!, "Restaurant"));
        }

        [Fact]
        public void CreateDish_SetsPreparationTimeFromMenuItem()
        {
            var menuItem = BuildMenuItem(prepMinutes: 25);
            var dish = _service.CreateDish(menuItem, "Restaurant") as DishBase;

            Assert.Equal(TimeSpan.FromMinutes(25), dish!.PreparationTime);
        }

        [Fact]
        public void CreateDish_PopulatesKitchenItems()
        {
            var item = new Item { Id = 1, Name = "Flour", Quantity = 50 };
            var kitchenItems = new List<KitchenItem>
            {
                new() { ItemId = 1, Item = item, MenuItemId = 1, QuantityRequired = 0.3 },
                new() { ItemId = 2, Item = new Item { Id = 2, Name = "Olive Oil" }, MenuItemId = 1, QuantityRequired = 0.03 }
            };

            var dish = _service.CreateDish(
                BuildMenuItem(kitchenItems: kitchenItems), "Restaurant");

            Assert.Equal(2, dish.GetKitchenItems().Count);
        }

        // Theory — same test for both service types
        [Theory]
        [InlineData("Restaurant")]
        [InlineData("Catering")]
        public void CreateDish_ValidServiceType_ReturnsIDish(string serviceType)
        {
            var dish = _service.CreateDish(BuildMenuItem(), serviceType);
            Assert.IsAssignableFrom<IDish>(dish);
        }

        [Fact]
        public void CreateDish_UnknownServiceType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(
                () => _service.CreateDish(BuildMenuItem(), "Takeaway"));
        }

        #endregion

        #region PrepareOrderAsync

        [Fact]
        public async Task PrepareOrderAsync_NullOrder_ThrowsArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _service.PrepareOrderAsync(null!));
        }

        [Fact]
        public async Task PrepareOrderAsync_ReturnsImmediately_BeforeDishesCook()
        {
            // Dishes have long prep time — method should return immediately
            var menuItem = BuildMenuItem(prepMinutes: 60);
            var dish = _service.CreateDish(menuItem, "Restaurant") as DishBase;

            var orderDetails = new OrderDetails
            {
                Dishes = [dish!],
                ClientId = 1,
                Client = new Client { Id = 1, Name = "Table_05" }
            };

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _service.PrepareOrderAsync(orderDetails);
            stopwatch.Stop();

            // Should return in well under 1 second despite 60 min prep time
            Assert.True(stopwatch.ElapsedMilliseconds < 1000);
        }

        [Fact]
        public async Task PrepareOrderAsync_EmptyDishes_CompletesWithoutError()
        {
            var orderDetails = new OrderDetails
            {
                Dishes = [],
                ClientId = 1,
                Client = new Client { Id = 1, Name = "Table_05" }
            };

            // Should not throw
            var exception = await Record.ExceptionAsync(
                () => _service.PrepareOrderAsync(orderDetails));

            Assert.Null(exception);
        }

        [Fact]
        public async Task PrepareOrderAsync_MultipleDishes_AllEventuallyCooked()
        {
            // Short prep times so test doesn't take long
            var dishes = new List<IDish>();
            for (int i = 0; i < 3; i++)
            {
                var menuItem = BuildMenuItem(prepMinutes: 0); // instant
                menuItem.PreparationTime = TimeSpan.FromMilliseconds(50);
                var dish = _service.CreateDish(menuItem, "Restaurant") as DishBase;
                dishes.Add(dish!);
            }

            var orderDetails = new OrderDetails
            {
                Dishes = dishes,
                ClientId = 1,
                Client = new Client { Id = 1, Name = "Table_05" }
            };

            await _service.PrepareOrderAsync(orderDetails);

            // Give background tasks time to complete
            await Task.Delay(200);

            Assert.All(dishes.Cast<DishBase>(), d => Assert.True(d.IsDishCooked));
        }

        #endregion
    }
}
