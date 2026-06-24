using CateringApp.Models.Interfaces;

namespace CateringApp.Models.Observers
{
    public class BillingObserver : IOrderEventObserver
    {
        public Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            Console.WriteLine($"[Billing] Order {e.OrderDetails.OrderId} " +
                          $"marked as unpaid — total: " +
                          $"{e.OrderDetails.Dishes.Count()} dishes");
            return Task.CompletedTask;
        }
    }
}
