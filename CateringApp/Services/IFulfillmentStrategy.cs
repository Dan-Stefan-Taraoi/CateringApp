using CateringApp.Models;
using CateringApp.Models.Enums;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public interface IFulfillmentStrategy
    {
        string ServiceType { get; }
        bool IsMethodSupported(CookingMethod method);
        OrderDetails PrepareOrder(IEnumerable<IDish> dishes);
    }
}