using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class FryDish : Item, IDish
    {
        public List<Ingredient> Ingredients { get; set; } = ;

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public IReadOnlyList<Ingredient> GetIngredients() => Ingredients;
    }
}
