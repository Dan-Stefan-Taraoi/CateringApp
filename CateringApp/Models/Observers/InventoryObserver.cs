using CateringApp.Data;
using CateringApp.Models.Interfaces;

namespace CateringApp.Models.Observers
{
    public class InventoryObserver : IOrderEventObserver
    {
        private readonly MyAppContext _context;

        public InventoryObserver(MyAppContext appContext)
        {
            _context = appContext ?? throw new ArgumentNullException(nameof(appContext));
        }

        public async Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            foreach (var dish in e.OrderDetails.Dishes)
            {
                foreach (var kitchenItem in dish.GetKitchenItems())
                {
                    var inventoryItem = await _context.Items.FindAsync(kitchenItem.ItemId);
                    if (inventoryItem is not null)
                    {
                        inventoryItem.Quantity -= kitchenItem.QuantityRequired;

                        // Flag as unavailable if stock hits zero
                        if (inventoryItem.Quantity <= 0)
                        {
                            inventoryItem.Quantity = 0;
                            inventoryItem.IsAvailable = false;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
