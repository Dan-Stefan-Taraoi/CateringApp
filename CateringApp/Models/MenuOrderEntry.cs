using System.ComponentModel.DataAnnotations;

namespace CateringApp.Models
{
    public class MenuOrderEntry
    {
        /// <summary>
        /// Gets or sets the unique identifier for the menu order entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated order. This represents the relationship between the menu order entry and the order it belongs to.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the order associated with this menu order entry.
        /// </summary>
        public Order Order { get; set; } = null!;

        /// <summary>
        /// Gets or sets the identifier of the associated menu item. This represents the relationship between the menu order entry and the menu item that has been ordered.
        /// </summary>
        public int? MenuItemId { get; set; }

        /// <summary>
        /// Gets or sets the menu item associated with this menu order entry. This represents the specific dish that has been ordered as part of the order.
        /// </summary>
        public MenuItem? MenuItem { get; set; } = null!;

        /// <summary>
        /// Gets or sets the name of the menu item. This is a required field that represents the name of the dish that has been ordered.
        /// It is used for display purposes and to provide clarity about what has been ordered in the context of the order details.
        /// </summary>
        [Required]
        public string MenuItemName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unit price of the menu item.
        /// This is a required field that represents the cost of a single unit of the menu item.
        /// </summary>
        [Range(0.01, double.MaxValue)]
        public double UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the menu item that has been ordered.
        /// This is a required field that represents how many units of the menu item have been ordered as part of the order.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the total price for this menu order entry, calculated as the unit price multiplied by the quantity.
        /// This represents the total cost for this specific menu item in the order.
        /// </summary>
        public double TotalPrice => UnitPrice * Quantity;
    }
}
