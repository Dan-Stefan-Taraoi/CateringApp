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
        /// Gets or sets the description of the item.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        public double Price { get; set; }

        public int? SerialNumberId { get; set; }

        public SerialNumber? SerialNumber {  get; set; }
    }
}
