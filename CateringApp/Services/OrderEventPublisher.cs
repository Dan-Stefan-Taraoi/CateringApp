using CateringApp.Models.Interfaces;
using CateringApp.Models.Observers;

namespace CateringApp.Services
{
    public class OrderEventPublisher
    {
        private readonly IEnumerable<IOrderEventObserver> _observers;

        public OrderEventPublisher(IEnumerable<IOrderEventObserver> observers)
        {
                _observers = observers ?? throw new ArgumentNullException(nameof(observers));
        }

        public async Task PublishAsync(OrderPlacedEvent e)
        {
            foreach (var observer in _observers)
            {
                await observer.OnOrderPlacedAsync(e);
            }
        }
    }
}
