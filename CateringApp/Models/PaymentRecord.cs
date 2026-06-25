using CateringApp.Models.Enums;

namespace CateringApp.Models
{
    public class PaymentRecord
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public bool IsPaid { get; set; } = false;

        public DateTime? PaidAt { get; set; }

        public PaymentMethod Method { get; set; }

        public double AmountPaid { get; set; }

        public string? Notes { get; set; }
    }
}
