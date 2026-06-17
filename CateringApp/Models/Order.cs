using System.ComponentModel.DataAnnotations;

namespace CateringApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsPaid { get; set; } = false;

        public int ClientId { get; set; }

        public Client Client { get; set; } = new Client();

        [Required]
        public string ServiceType { get; set; } = string.Empty;

        public bool RequiresTransport { get; set; }

        public bool IsBulkPackaged { get; set; }

        public List<MenuOrderEntry> Entries { get; set; } = [];

        // Computed — not stored in DB
        public double Total => Entries.Sum(e => e.TotalPrice);
    }
}
