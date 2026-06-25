using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class DishBase : IDish
    {
        public List<KitchenItem> KitchenItems { get; set; } = [];

        public bool IsDishCooked { get; private set; }

        public TimeSpan PreparationTime { get; set; }

        private void OnCountdownFinished()
        {
            IsDishCooked = true;
        }

        public void AddKitchenItem(KitchenItem kitchenItem)
        {
            ArgumentNullException.ThrowIfNull(kitchenItem);
            KitchenItems.Add(kitchenItem);
        }

        public IReadOnlyList<KitchenItem> GetKitchenItems()
            => KitchenItems.AsReadOnly();

        public async Task PrepareAsync()
        {
            await Task.Delay(PreparationTime);
            OnCountdownFinished();
        }
    }
}
