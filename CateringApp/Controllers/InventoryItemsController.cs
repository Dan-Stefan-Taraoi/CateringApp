using CateringApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace CateringApp.Controllers
{
    public class InventoryItemsController : Controller
    {
        private readonly MyAppContext _context;

        public InventoryItemsController(MyAppContext context)
        {
                _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
