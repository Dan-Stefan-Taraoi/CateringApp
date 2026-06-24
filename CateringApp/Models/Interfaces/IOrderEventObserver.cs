using CateringApp.Models.Observers;

namespace CateringApp.Models.Interfaces
{
    public interface IOrderEventObserver
    {
        public Task OnOrderPlacedAsync(OrderPlacedEvent e);
    }
}
