using CateringApp.Models;

namespace CateringApp.Models.Interfaces
{
    public interface IOrderEventObserver
    {
        public Task OnOrderPlacedAsync(OrderPlacedEvent e);
    }
}
