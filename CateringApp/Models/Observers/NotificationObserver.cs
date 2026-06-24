using CateringApp.Models.Interfaces;

namespace CateringApp.Models.Observers
{
    public class NotificationObserver : IOrderEventObserver
    {
        public Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            Console.WriteLine($"[Notification] Order {e.OrderDetails.OrderId} placed " +
                          $"for client {e.OrderDetails.Client.Name} " +
                          $"at {DateTime.UtcNow:HH:mm:ss}");
            return Task.CompletedTask;
        }
    }
}
