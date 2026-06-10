using System.ComponentModel.DataAnnotations.Schema;

namespace CateringApp.Models
{
    public class HardwareItem : InventoryItem
    {
        public int? SerialNumberId { get; set; }
        [ForeignKey("SerialNumberId")]
        public SerialNumber? SerialNumber { get; set; }
    }
}