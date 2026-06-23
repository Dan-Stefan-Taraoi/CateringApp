using CateringApp.Models.Interfaces;
using CateringApp.Services;

namespace CateringApp.Models
{
    public class KitchenObserver : IOrderEventObserver
    {
        private readonly DishService _dishService;

        public KitchenObserver(DishService dishService)
        {
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));  
        }

        public Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
