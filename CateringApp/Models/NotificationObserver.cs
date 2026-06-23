using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class NotificationObserver : IOrderEventObserver
    {
        public Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
