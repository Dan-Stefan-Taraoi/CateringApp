namespace CateringApp.Models.Interfaces
{
    public interface IDish
    {
        public List<Item> Ingredients { get; set; }

        public void AddIngredient(Item ingredient);

        public void RemoveIngredient(Item ingredient);

        public Task PrepareAsync();

        public IReadOnlyList<Item> GetIngredients();
    }
}
