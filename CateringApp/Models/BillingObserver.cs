using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class BillingObserver : IOrderEventObserver
    {
        public Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
