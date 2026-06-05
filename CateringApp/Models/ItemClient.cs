namespace CateringApp.Models
{
    public class ItemClient
    {
        public int ItemId { get; set; }

        public Item Item { get; set; } = null!;

        public int ClientId { get; set; }

        public Client Client { get; set; } = null!;
    }
}
