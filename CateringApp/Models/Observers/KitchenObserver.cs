using CateringApp.Models.Interfaces;
using CateringApp.Services;

namespace CateringApp.Models.Observers
{
    public class KitchenObserver : IOrderEventObserver
    {
        private readonly DishService _dishService;

        public KitchenObserver(DishService dishService)
        {
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));  
        }

        public async Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            await _dishService.PrepareOrderAsync(e.OrderDetails);
        }
    }
}
