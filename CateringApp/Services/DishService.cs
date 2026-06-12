using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class DishService : IDishService
    {
        private readonly IKitchenFactory _kitchen;

        public DishService(IKitchenFactory kitchenFactory)
        {
            _kitchen = kitchenFactory ?? throw new ArgumentNullException(nameof(kitchenFactory));
        }

        public void PrepareOrder(OrderDetails order)
        {
            ArgumentNullException.ThrowIfNull(order);

            // Kitchen context determined by injected IKitchenFactory (Restaurant or Catering)

            foreach (var dish in order.Dishes)
            {
                dish.Prepare();
            }
        }

        public IDish CreateDish(MenuItem menuItem)
        {
            ArgumentNullException.ThrowIfNull(menuItem);

            return _kitchen.CreateDish(menuItem);
        }
    }
}