using System.ComponentModel.DataAnnotations;

namespace CateringApp.Models
{
    public class Client
    {
        /// <summary>
        /// Gets or sets the unique identifier for the client.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the client. This represents the name of the customer or organization that is placing an order with the catering service.<br/>
        /// </summary>
        [Required(ErrorMessage = "Client name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the client. This is a mandatory field that can be used to store the physical address of the client for delivery or billing purposes.
        /// </summary>
        [Required(ErrorMessage = "Client address is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Address must be between 2 and 100 characters.")]
        public string Address { get; set; } = string.Empty;
    }
}
