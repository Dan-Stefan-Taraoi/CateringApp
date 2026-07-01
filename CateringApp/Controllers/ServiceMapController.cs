using CateringApp.Data;
using CateringApp.Models;
using CateringApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Controllers
{
    public class ServiceMapController : Controller
    {
        private readonly MyAppContext _context;

        public ServiceMapController(MyAppContext myAppContext)
        {
                _context = myAppContext ?? throw new ArgumentNullException(nameof(myAppContext));
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients
                .ToListAsync();

            List<TableStatusViewModel> tabeStatusViewModels = [];
            foreach (var client in clients)
            {
                var tabeStatusViewModel = new TableStatusViewModel()
                {
                    Client = client,
                    ActiveOrder = await _context.Orders
                        .Include(o => o.PaymentRecord)
                        .FirstOrDefaultAsync(o => o.ClientId == client.Id)
                };

                tabeStatusViewModels.Add(tabeStatusViewModel);
            }

            return View(tabeStatusViewModels);
        }
    }
}
