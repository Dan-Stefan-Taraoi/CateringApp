using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class OrderDetails
    {
        public IEnumerable<IDish> Dishes { get; set; } = [];
        public bool IsTableService { get; set; }
        public bool IsBulkPackaged { get; set; }
        public bool RequiresTransport { get; set; }
    }
}