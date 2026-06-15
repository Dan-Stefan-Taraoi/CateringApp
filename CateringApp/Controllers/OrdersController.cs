using CateringApp.Data;
using CateringApp.Models;
using CateringApp.Models.Interfaces;
using CateringApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MyAppContext _context;
        private readonly DishService _dishService;

        public OrdersController(MyAppContext context, DishService dishService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dishService = dishService ?? throw new ArgumentNullException(nameof(dishService));
        }

        #region Index

        // GET: Orders — active orders overview
        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Place Order

        // GET: Orders/Place — show menu selection form
        public async Task<IActionResult> Place()
        {
            var menuItems = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                    .ThenInclude(ki => ki.Item)
                .ToListAsync();

            var clients = await _context.Clients.ToListAsync();

            ViewBag.Clients = clients;

            return View(menuItems);
        }

        // POST: Orders/Place — build order from selected items
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Place(
            List<int> selectedMenuItemIds,
            string serviceType,
            int clientId,
            string? location)
        {
            if (selectedMenuItemIds == null || !selectedMenuItemIds.Any())
            {
                ModelState.AddModelError("", "Please select at least one menu item.");
                return await ReloadPlaceView();
            }

            // Load selected MenuItems with their ingredients
            var menuItems = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                    .ThenInclude(ki => ki.Item)
                .Where(m => selectedMenuItemIds.Contains(m.Id))
                .ToListAsync();

            var client = await _context.Clients.FindAsync(clientId);
            if (client == null) return NotFound();

            // Create dishes via DishService (uses injected factory)
            var dishes = menuItems
                .Select(m => _dishService.CreateDish(m))
                .ToList();

            // Build OrderDetails
            var order = new OrderDetails
            {
                Dishes = dishes,
                Client = client,
                ClientId = clientId,
                IsTableService = serviceType == "Restaurant",
                IsBulkPackaged = serviceType == "Catering",
                RequiresTransport = serviceType == "Catering",
            };

            // Process the order through DishService
            await _dishService.PrepareOrderAsync(order);

            return View("Confirmation", order);
        }

        #endregion

        #region Helpers

        private async Task<IActionResult> ReloadPlaceView()
        {
            var menuItems = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                    .ThenInclude(ki => ki.Item)
                .ToListAsync();

            ViewBag.Clients = await _context.Clients.ToListAsync();

            return View("Place", menuItems);
        }

        #endregion
    }
}