namespace CateringApp.Models
{
    public class KitchenItem
    {
        public int ItemId { get; set; }

        public Item Item { get; set; } = null!;

        public int MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; } = null!;

        public double QuantityRequired { get; set; }
    }
}
