using CateringApp.Models.Enums;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public interface IKitchenFactory
    {
        public IDish CreateDish(CookingMethod method, string name, string description, double price);
    }
}
