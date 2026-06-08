using System.ComponentModel.DataAnnotations.Schema;

namespace CateringApp.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
    }
}
