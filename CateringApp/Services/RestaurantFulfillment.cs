using CateringApp.Models;
using CateringApp.Models.Enums;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class RestaurantFulfillment : IFulfillmentStrategy
    {
        public string ServiceType => "Restaurant";

        public bool IsMethodSupported(CookingMethod method) => true;

        public OrderDetails PrepareOrder(IEnumerable<IDish> dishes)
        {
            return new OrderDetails
            {
                Dishes = dishes,
                IsTableService = true,
                IsBulkPackaged = false,
                RequiresTransport = false
            };
        }
    }
}