using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class RestaurantDish : IDish
    {
        public List<Item> Ingredients { get; set; } = [];

        public MenuItem MenuItem { get; set; } = new MenuItem();

        public void AddIngredient(Item ingredient)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<Item> GetIngredients()
        {
            throw new NotImplementedException();
        }

        public void Prepare()
        {
            throw new NotImplementedException();
        }

        public void RemoveIngredient(Item ingredient)
        {
            throw new NotImplementedException();
        }
    }
}
