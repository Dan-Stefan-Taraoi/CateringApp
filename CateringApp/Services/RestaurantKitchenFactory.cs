using CateringApp.Models;
using CateringApp.Models.Enums;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class RestaurantKitchenFactory : IKitchenFactory
    {
        private readonly Dictionary<CookingMethod, Func<string, string, double, IDish>> _creators;

        public RestaurantKitchenFactory()
        {
            _creators = new()
            {
                { CookingMethod.Fried, (n, d, p) => new FryDish  { Name = n, Description = d, Price = p } },
                { CookingMethod.Baked, (n, d, p) => new BakedDish { Name = n, Description = d, Price = p } },
                { CookingMethod.Cold,  (n, d, p) => new ColdDish  { Name = n, Description = d, Price = p } },
            };
        }

        public IDish CreateDish(CookingMethod method, string name, string description, double price)
        {
            if (!_creators.TryGetValue(method, out var creator))
                throw new ArgumentException($"Cooking method {method} not supported in restaurant kitchen");

            return creator(name, description, price);
        }
    }
}