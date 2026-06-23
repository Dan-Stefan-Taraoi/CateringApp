using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class OrderPlacedEvent
    {
        public Order Order { get; }

        public List<IDish> Dishes { get; }

        public OrderPlacedEvent(Order order, List<IDish> dishes)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
            Dishes = dishes ?? throw new ArgumentNullException(nameof (dishes));
        }
    }
}
