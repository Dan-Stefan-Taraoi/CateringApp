using CateringApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CateringApp.Models
{
    /// <summary>
    /// Represents a menu item, including its identifier, name, description, cooking method, serving size, and
    /// associated kitchen items.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the menu dish.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the menu dish.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the menu dish.
        /// </summary>.
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the cooking method for the menu dish.
        /// </summary>
        [Display(Name = "Cooking Method")]
        public CookingMethod CookingMethod { get; set; }

        /// <summary>
        /// Gets or sets the number of servings for the menu dish. This indicates how many people the dish can serve.<br/>
        /// This information can be useful for customers when ordering, as it helps them understand how much food they will receive and whether it will be sufficient for their needs.
        /// </summary>
        [Range(1, 100, ErrorMessage = "Serves must be between 1 and 100.")]
        public int Serves { get; set; } = 1;

        /// <summary>
        /// Gets or sets the price of the menu dish. This represents the cost that customers will pay for ordering this dish.<br/>
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the preparation time for the menu dish. This represents the estimated time required to prepare the dish, which can be useful for customers when ordering and for kitchen staff when managing orders and scheduling.<br/>
        /// </summary>
        [Display(Name = "Preparation Time")]
        public TimeSpan PreparationTime { get; set; }

        /// <summary>
        /// Gets or sets the list of kitchen items (ingredients and tools) that are used to prepare the menu dish.
        /// This represents the components that make up the dish and can be used to calculate the total cost, nutritional information, and to manage inventory.<br/> 
        /// </summary>
        public List<KitchenItem>? KitchenItems{ get; set; }
    }
}