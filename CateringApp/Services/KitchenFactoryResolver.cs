using CateringApp.Models.Enums;

namespace CateringApp.Services
{
    public class KitchenFactoryResolver
    {
        private readonly IEnumerable<IKitchenFactory> _factories;

        public KitchenFactoryResolver(IEnumerable<IKitchenFactory> kitchenFactories)
        {
                _factories = kitchenFactories ?? throw new ArgumentNullException(nameof(kitchenFactories));
        }

        public IKitchenFactory GetFactory(string serviceType)
        {
            return serviceType switch
            {
                "Restaurant" => _factories.OfType<RestaurantKitchenFactory>().First(),
                "Catering" => _factories.OfType<CateringKitchenFactory>().First(),
                _ => throw new ArgumentException($"Unknown service type: {serviceType}")
            };
        }
    }
}
