using CateringApp.Models;
using CateringApp.Models.Builders;

namespace CateringApp.Tests.Builder
{
    public class OrderDetailsBuilderTests
    {
        private readonly Client _client = new() { Id = 1, Name = "Table_05" };

        [Fact]
        public void Build_Restaurant_SetsCorrectFlags()
        {
            var details = new OrderDetailsBuilder()
                .WithClient(_client)
                .WithServiceType("Restaurant")
                .WithDishes([])
                .Build();

            Assert.True(details.IsTableService);
            Assert.False(details.IsBulkPackaged);
            Assert.False(details.RequiresTransport);
        }

        [Fact]
        public void Build_Catering_SetsCorrectFlags()
        {
            var details = new OrderDetailsBuilder()
                .WithClient(_client)
                .WithServiceType("Catering")
                .WithDishes([])
                .Build();

            Assert.False(details.IsTableService);
            Assert.True(details.IsBulkPackaged);
            Assert.True(details.RequiresTransport);
        }

        [Fact]
        public void Build_SetsClientIdFromClient()
        {
            var details = new OrderDetailsBuilder()
                .WithClient(_client)
                .WithServiceType("Restaurant")
                .WithDishes([])
                .Build();

            Assert.Equal(1, details.ClientId);
        }
    }
}
