namespace CateringApp.Models.ViewModels
{
    public class TableStatusViewModel
    {
        public Client Client { get; set; } = null!;
        public Order? ActiveOrder { get; set; }
    }
}
