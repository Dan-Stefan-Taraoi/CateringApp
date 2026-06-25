namespace CateringApp.Models.Interfaces
{
    public interface IDish
    {
        List<KitchenItem> KitchenItems { get; set; }
        void AddKitchenItem(KitchenItem kitchenItem);
        IReadOnlyList<KitchenItem> GetKitchenItems();
        Task PrepareAsync();
    }
}
