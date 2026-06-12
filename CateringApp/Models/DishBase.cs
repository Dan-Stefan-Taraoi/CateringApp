using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class DishBase : IDish
    {
        public List<Item> Ingredients { get; set; } = [];

        public void AddIngredient(Item ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public IReadOnlyList<Item> GetIngredients()
        {
            return Ingredients.AsReadOnly();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void RemoveIngredient(Item ingredient)
        {
            ArgumentNullException.ThrowIfNull(ingredient);

            var foundIngredient = Ingredients.FirstOrDefault(i => i.Id == ingredient.Id);
            if (foundIngredient != null)
            {
                Ingredients.Remove(foundIngredient);
            }
        }
    }
}
