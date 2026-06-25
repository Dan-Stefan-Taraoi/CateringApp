using System.ComponentModel.DataAnnotations;

namespace CateringApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Location { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; } = new Client();

        [Required]
        public string ServiceType { get; set; } = string.Empty;

        public PaymentRecord? PaymentRecord { get; set; }

        public bool RequiresTransport { get; set; }

        public bool IsBulkPackaged { get; set; }

        public List<MenuOrderEntry> Entries { get; set; } = [];

        // Computed — not stored in DB
        public double Total => Entries.Sum(e => e.TotalPrice);

        public bool IsPaid => PaymentRecord?.IsPaid ?? false;

        // Protected constructor — forces factory method usage
        protected Order() { }

        public static Order Create(
            Client client,
            string serviceType,
            List<MenuItem> menuItems,
            string? location = null)
        {
            return new Order
            {
                CreatedAt = DateTime.UtcNow,
                ClientId = client.Id,
                Client = client,
                ServiceType = serviceType,
                RequiresTransport = serviceType == "Catering",
                IsBulkPackaged = serviceType == "Catering",
                Location = location,
                Entries = [.. menuItems.Select(m => new MenuOrderEntry
                {
                    MenuItemId = m.Id,
                    MenuItemName = m.Name,
                    UnitPrice = m.Price,
                    Quantity = 1
                })]
            };
        }
    }
}
