using CateringApp.Data;
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

        /// <summary>
        /// Prepares all dishes in the order asynchronously.
        /// </summary>
        /// <param name="order">The whole order details.</param>
        /// <returns></returns>
        public Task PrepareOrderAsync(OrderDetails order)
        {
            ArgumentNullException.ThrowIfNull(order);

            // Start cooking in background — don't await
            _ = Task.WhenAll(order.Dishes
                .Select(d => d.PrepareAsync()))
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        Console.WriteLine($"[Kitchen] Error: {t.Exception?.Message}");
                });

            return Task.CompletedTask;
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