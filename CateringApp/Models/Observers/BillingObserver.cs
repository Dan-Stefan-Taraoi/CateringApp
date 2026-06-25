using CateringApp.Data;
using CateringApp.Models.Enums;
using CateringApp.Models.Interfaces;

namespace CateringApp.Models.Observers
{
    public class BillingObserver : IOrderEventObserver
    {
        private readonly MyAppContext _context;

        public BillingObserver(MyAppContext context)
        {
            _context = context;
        }

        public async Task OnOrderPlacedAsync(OrderPlacedEvent e)
        {
            var payment = new PaymentRecord
            {
                OrderId = e.OrderDetails.OrderId,
                IsPaid = false,
                AmountPaid = 0,
                Method = PaymentMethod.Cash  // default
            };

            _context.PaymentRecords.Add(payment);
            await _context.SaveChangesAsync();
        }
    }
}
