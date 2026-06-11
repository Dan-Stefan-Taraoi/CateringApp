using CateringApp.Models.Enums;

namespace CateringApp.Models
{
    public class MenuItem : Item
    {
        public CookingMethod CookingMethod { get; set; }

        public int Serves { get; set; } = 1;
    }
}