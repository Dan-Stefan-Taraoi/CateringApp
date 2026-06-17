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

        // public List<ItemClient>? ItemClients { get; set; }
    }
}
