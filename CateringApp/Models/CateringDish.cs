using CateringApp.Models.Interfaces;

namespace CateringApp.Models
{
    public class CateringDish : IDish
    {
        public List<Item> Ingredients { get; set; } = [];

        public string Location { get; set; } = string.Empty;

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
