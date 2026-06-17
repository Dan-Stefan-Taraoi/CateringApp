
using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class OrderDetails
    {
        public IEnumerable<IDish> Dishes { get; set; } = [];

        public int ClientId { get; set; }
        public Client Client { get; set; } = new Client();


        public bool IsTableService { get; set; }

        public bool IsBulkPackaged { get; set; }

        public bool RequiresTransport { get; set; }
    }
}