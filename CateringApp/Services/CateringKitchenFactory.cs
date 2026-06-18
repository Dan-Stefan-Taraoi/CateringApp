using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public class CateringKitchenFactory : IKitchenFactory
    {
        public IDish CreateDish(MenuItem menuItem)
        {
            var dish = new CateringDish
            {
                PreparationTime = menuItem.PreparationTime
            };

            if (menuItem.KitchenItems != null)
            {
                foreach (var kitchenItem in menuItem.KitchenItems)
                {
                    dish.AddKitchenItem(kitchenItem);
                }
            }

            return dish;
        }
    }
}
