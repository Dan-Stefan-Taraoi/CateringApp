using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class CateringKitchenFactory : IKitchenFactory
    {
        public IDish CreateDish(MenuItem menuItem, string? location = null)
        {
            var dish = new CateringDish
            {
                PreparationTime = menuItem.PreparationTime,
                Location = location ?? string.Empty
            };

            if (menuItem.KitchenItems != null)
                foreach (var ki in menuItem.KitchenItems)
                    dish.AddKitchenItem(ki);

            return dish;
        }
    }
}
