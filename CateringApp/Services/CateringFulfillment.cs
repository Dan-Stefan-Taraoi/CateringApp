using CateringApp.Models;
using CateringApp.Models.Enums;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class CateringFulfillment : IFulfillmentStrategy
    {
        public string ServiceType => "Catering";

        public bool IsMethodSupported(CookingMethod method) => true; // full kitchen available

        public OrderDetails PrepareOrder(IEnumerable<IDish> dishes)
        {
            return new OrderDetails
            {
                Dishes = dishes,
                IsTableService = false,
                IsBulkPackaged = true,
                RequiresTransport = true
            };
        }
    }
}