namespace CateringApp.Models.Interfaces
{
    public interface IDish
    {
        public List<Ingredient> Ingredients { get; set; }

        public void AddIngredient(Ingredient ingredient);

        public IReadOnlyList<Ingredient> GetIngredients();
    }
}
