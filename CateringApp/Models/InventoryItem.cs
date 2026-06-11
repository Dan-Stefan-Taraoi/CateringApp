using System.ComponentModel.DataAnnotations.Schema;

namespace CateringApp.Models
{
    public class InventoryItem : Item
    {
        public int Quantity { get; set; }

        public string? Location { get; set; }
    }
}