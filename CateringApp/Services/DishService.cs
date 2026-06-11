using CateringApp.Models;
using CateringApp.Models.Enums;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class DishService
    {
        private readonly IKitchenFactory _kitchen;
        private readonly IFulfillmentStrategy _fulfillment;

        public DishService(IKitchenFactory kitchen, IFulfillmentStrategy fulfillment)
        {
            _kitchen = kitchen ?? throw new ArgumentNullException(nameof(kitchen));
            _fulfillment = fulfillment ?? throw new ArgumentNullException(nameof(fulfillment));
        }

        public IDish CreateDish(CookingMethod method, string name, string description, double price)
        {
            if (!_fulfillment.IsMethodSupported(method))
                throw new InvalidOperationException(
                    $"{method} is not supported for {_fulfillment.ServiceType} service");

            return _kitchen.CreateDish(method, name, description, price);
        }

        public OrderDetails PrepareOrder(IEnumerable<IDish> dishes)
        {
            return _fulfillment.PrepareOrder(dishes);
        }
    }
}