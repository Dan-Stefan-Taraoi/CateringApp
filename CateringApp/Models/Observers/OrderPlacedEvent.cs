using CateringApp.Models.Interfaces;

namespace CateringApp.Models.Observers
{
    public class OrderPlacedEvent
    {
        public OrderDetails OrderDetails { get; }

        public OrderPlacedEvent(OrderDetails orderDetails)
        {
            OrderDetails = orderDetails ?? throw new ArgumentNullException(nameof(orderDetails));
        }
    }
}
