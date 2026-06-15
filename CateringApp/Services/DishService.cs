using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class DishService
    {
        private readonly IKitchenFactory _kitchen;

        public DishService(IKitchenFactory kitchenFactory)
        {
            _kitchen = kitchenFactory ?? throw new ArgumentNullException(nameof(kitchenFactory));
        }

        public async Task PrepareOrderAsync(OrderDetails order)
        {
            ArgumentNullException.ThrowIfNull(order);

            // Kitchen context determined by injected IKitchenFactory (Restaurant or Catering)

            var cookingDishes = order.Dishes.Select(order => order.PrepareAsync()).ToList();
            await Task.WhenAll(cookingDishes);
        }

        public IDish CreateDish(MenuItem menuItem)
        {
            ArgumentNullException.ThrowIfNull(menuItem);

            return _kitchen.CreateDish(menuItem);
        }
    }
}