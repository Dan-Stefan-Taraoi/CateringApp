using System.ComponentModel.DataAnnotations.Schema;

namespace CateringApp.Models
{
    /// <summary>
    /// This is equivalent to a table in a database.
    /// It is a model that represents an item in the catering app.
    /// It can be used to store information about the item such as its name, price, description, etc.
    /// It can also be used to create relationships with other models such as orders, categories, etc.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the item per unit. This represents the cost of one unit of the item, which can be used for pricing and billing purposes.<br/>
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the stored quantity of the item. This represents how much of the item is currently available in stock or inventory.<br/>
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit of measurement for the item quantity (e.g., grams, liters, pieces).
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        public List<KitchenItem>? KitchenItems { get; set; }
    }
}
