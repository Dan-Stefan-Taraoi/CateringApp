using CateringApp.Services;

namespace CateringApp.Tests.Factories
{
    public class KitchenFactoryResolverTests
    {
        private readonly KitchenFactoryResolver _resolver;

        public KitchenFactoryResolverTests()
        {
            var factories = new List<IKitchenFactory>
        {
            new RestaurantKitchenFactory(),
            new CateringKitchenFactory()
        };
            _resolver = new KitchenFactoryResolver(factories);
        }

        [Fact]
        public void Resolve_Restaurant_ReturnsRestaurantFactory()
        {
            var factory = _resolver.GetFactory("Restaurant");
            Assert.IsType<RestaurantKitchenFactory>(factory);
        }

        [Fact]
        public void Resolve_Catering_ReturnsCateringFactory()
        {
            var factory = _resolver.GetFactory("Catering");
            Assert.IsType<CateringKitchenFactory>(factory);
        }

        [Fact]
        public void Resolve_UnknownType_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _resolver.GetFactory("Unknown"));
        }
    }
}
