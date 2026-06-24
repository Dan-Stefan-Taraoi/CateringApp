using CateringApp.Models.Interfaces;

namespace CateringApp.Models.Builders
{
    public class OrderDetailsBuilder
    {
        private List<IDish> _dishes = [];
        private Client _client = new();
        private string _serviceType = string.Empty;
        private string? _location;

        public OrderDetailsBuilder WithDishes(List<IDish> dishes)
        {
            _dishes = dishes;
            return this;
        }

        public OrderDetailsBuilder WithClient(Client client)
        {
            _client = client;
            return this;
        }

        public OrderDetailsBuilder WithServiceType(string serviceType)
        {
            _serviceType = serviceType;
            return this;
        }

        public OrderDetailsBuilder WithLocation(string? location)
        {
            _location = location;
            return this;
        }

        public OrderDetails Build()
        {
            return new OrderDetails
            {
                Dishes = _dishes,
                Client = _client,
                ClientId = _client.Id,
                IsTableService = _serviceType == "Restaurant",
                IsBulkPackaged = _serviceType == "Catering",
                RequiresTransport = _serviceType == "Catering",
            };
        }
    }
}
