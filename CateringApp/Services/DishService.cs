using CateringApp.Data;
using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class DishService
    {
        private readonly KitchenFactoryResolver _resolver;

        public DishService(KitchenFactoryResolver resolver)
        {
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
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
            var cookingTasks = order.Dishes
            .Select(d => d.PrepareAsync())
            .ToList();

            _ = Task.WhenAll(cookingTasks).ContinueWith(t =>
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
        public IDish CreateDish(MenuItem menuItem, string serviceType, string? location = null)
        {
            ArgumentNullException.ThrowIfNull(menuItem);
            ArgumentException.ThrowIfNullOrWhiteSpace(serviceType);

            var factory = _resolver.GetFactory(serviceType);
            return factory.CreateDish(menuItem, location);
        }
    }
}