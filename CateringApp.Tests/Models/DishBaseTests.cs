using CateringApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringApp.Tests.Models
{
    public class DishBaseTests
    {
        private KitchenItem BuildKitchenItem(int id, string name) => new()
        {
            ItemId = id,
            Item = new Item { Id = id, Name = name },
            MenuItemId = 1,
            QuantityRequired = 0.1
        };

        [Fact]
        public void AddKitchenItem_IncreasesCount()
        {
            var dish = new RestaurantDish();
            dish.AddKitchenItem(BuildKitchenItem(1, "Flour"));

            Assert.Single(dish.GetKitchenItems());
        }

        [Fact]
        public void AddKitchenItem_Null_ThrowsArgumentNullException()
        {
            var dish = new RestaurantDish();
            Assert.Throws<ArgumentNullException>(() => dish.AddKitchenItem(null!));
        }

        [Fact]
        public void GetKitchenItems_ReturnsReadOnlyList()
        {
            var dish = new RestaurantDish();
            dish.AddKitchenItem(BuildKitchenItem(1, "Flour"));

            var items = dish.GetKitchenItems();
            Assert.IsAssignableFrom<IReadOnlyList<KitchenItem>>(items);
        }

        [Fact]
        public async Task PrepareAsync_SetsIsDishCooked()
        {
            var dish = new RestaurantDish
            {
                PreparationTime = TimeSpan.FromMilliseconds(50) // short for testing
            };

            await dish.PrepareAsync();

            Assert.True(dish.IsDishCooked);
        }
    }
}
