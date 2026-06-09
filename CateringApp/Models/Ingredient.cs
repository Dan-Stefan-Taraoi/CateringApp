namespace CateringApp.Models
{
    public class Ingredient : Item
    {
        public string Unit { get; set; } = string.Empty;

        public double Quantity { get; set; }
    }
}
