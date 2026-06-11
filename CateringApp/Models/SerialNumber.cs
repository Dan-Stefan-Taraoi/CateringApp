using System.ComponentModel.DataAnnotations.Schema;

namespace CateringApp.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? HardwareItemId { get; set; }
        [ForeignKey("HardwareItemId")]
        public HardwareItem? HardwareItem { get; set; }
    }
}