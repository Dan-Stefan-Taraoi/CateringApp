using CateringApp.Models;
using CateringApp.Services;

namespace CateringApp.Tests.Factories
{
    public class RestaurantKitchenFactoryTests
    {
        private readonly RestaurantKitchenFactory _factory = new();

        private MenuItem BuildMenuItem(List<KitchenItem>? kitchenItems = null)
        {
            return new MenuItem
            {
                Id = 1,
                Name = "Pizza Margherita",
                PreparationTime = TimeSpan.FromMinutes(20),
                KitchenItems = kitchenItems ?? []
            };
        }

        [Fact]
        public void CreateDish_ReturnsRestaurantDish()
        {
            var dish = _factory.CreateDish(BuildMenuItem());
            Assert.IsType<RestaurantDish>(dish);
        }

        [Fact]
        public void CreateDish_SetsPreparationTimeFromMenuItem()
        {
            var menuItem = BuildMenuItem();
            var dish = _factory.CreateDish(menuItem) as DishBase;

            Assert.Equal(TimeSpan.FromMinutes(20), dish!.PreparationTime);
        }

        [Fact]
        public void CreateDish_PopulatesKitchenItemsFromMenuItem()
        {
            var item = new Item { Id = 1, Name = "Flour" };
            var kitchenItems = new List<KitchenItem>
        {
            new() { ItemId = 1, Item = item, MenuItemId = 1, QuantityRequired = 0.3 }
        };

            var dish = _factory.CreateDish(BuildMenuItem(kitchenItems));

            Assert.Single(dish.GetKitchenItems());
            Assert.Equal("Flour", dish.GetKitchenItems()[0].Item.Name);
        }

        [Fact]
        public void CreateDish_WithNoKitchenItems_ReturnsEmptyIngredients()
        {
            var dish = _factory.CreateDish(BuildMenuItem());
            Assert.Empty(dish.GetKitchenItems());
        }
    }
}
