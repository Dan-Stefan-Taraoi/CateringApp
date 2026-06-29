using CateringApp.Models;
using CateringApp.Services;

namespace CateringApp.Tests.Factories
{
    public class CateringKitchenFactoryTests
    {
        private readonly CateringKitchenFactory _factory = new();

        [Fact]
        public void CreateDish_ReturnsCateringDish()
        {
            var dish = _factory.CreateDish(new MenuItem { Name = "Lasagne" });
            Assert.IsType<CateringDish>(dish);
        }

        [Fact]
        public void CreateDish_SetsLocation_WhenProvided()
        {
            var dish = _factory.CreateDish(
                new MenuItem { Name = "Lasagne" },
                "Messe Nürnberg") as CateringDish;

            Assert.Equal("Messe Nürnberg", dish!.Location);
        }

        [Fact]
        public void CreateDish_LocationIsEmpty_WhenNotProvided()
        {
            var dish = _factory.CreateDish(new MenuItem { Name = "Lasagne" }) as CateringDish;
            Assert.Equal(string.Empty, dish!.Location);
        }
    }
}
