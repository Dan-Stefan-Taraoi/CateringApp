using CateringApp.Data;
using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class DishService
    {
        private readonly IKitchenFactory _kitchen;

        private readonly MyAppContext _context;

        public DishService(IKitchenFactory kitchenFactory, MyAppContext appContext)
        {
            _kitchen = kitchenFactory ?? throw new ArgumentNullException(nameof(kitchenFactory));
            _context = appContext ?? throw new ArgumentNullException(nameof(appContext));
        }

        /// <summary>
        /// Prepares all dishes in the order asynchronously.
        /// </summary>
        /// <param name="order">The whole order details.</param>
        /// <returns></returns>
        public async Task PrepareOrderAsync(OrderDetails order)
        {
            ArgumentNullException.ThrowIfNull(order);

            // Cook all dishes concurrently
            var cookingTasks = order.Dishes
                .Select(d => d.PrepareAsync())
                .ToList();
            await Task.WhenAll(cookingTasks);

            // Deduct inventory
            foreach (var dish in order.Dishes)
            {
                foreach (var kitchenItem in dish.GetKitchenItems())
                {
                    var inventoryItem = await _context.Items.FindAsync(kitchenItem.ItemId);
                    if (inventoryItem != null)
                        inventoryItem.Quantity -= kitchenItem.QuantityRequired;
                }
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Creates a dish based on the provided menu item using the kitchen factory.
        /// </summary>
        /// <param name="menuItem">The menu item that is also available in the UI.</param>
        /// <returns></returns>
        public IDish CreateDish(MenuItem menuItem)
        {
            ArgumentNullException.ThrowIfNull(menuItem);

            return _kitchen.CreateDish(menuItem);
        }
    }
}