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

        public async Task<IActionResult> History()
        {
            var orders = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Entries)
                    .ThenInclude(e => e.MenuItem)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return View(orders);
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

            // 1. Load selected MenuItems with their ingredients
            var menuItems = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                    .ThenInclude(ki => ki.Item)
                .Where(m => selectedMenuItemIds.Contains(m.Id))
                .ToListAsync();

            // 2. Find client
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null) return NotFound();

            // 3. Create dishes via DishService (uses injected factory)
            var dishes = menuItems
                .Select(m => _dishService.CreateDish(m))
                .ToList();

            // 4. Build OrderDetails
            var orderDetails = new OrderDetails
            {
                Dishes = dishes,
                Client = client,
                ClientId = clientId,
                IsTableService = serviceType == "Restaurant",
                IsBulkPackaged = serviceType == "Catering",
                RequiresTransport = serviceType == "Catering",
            };

            // 5. Process the order through DishService
            await _dishService.PrepareOrderAsync(orderDetails);

            // 6. Convert to Order (persistent — DB)
            var order = new Order
            {
                CreatedAt = DateTime.UtcNow,
                ClientId = clientId,
                Client = client,
                ServiceType = serviceType,
                RequiresTransport = serviceType == "Catering",
                IsBulkPackaged = serviceType == "Catering",
                Entries = [.. menuItems.Select(m => new MenuOrderEntry
                {
                    MenuItemId = m.Id,
                    MenuItemName = m.Name,    // snapshot
                    UnitPrice = m.Price,      // snapshot
                    Quantity = 1              // for now — later from form input
                })]
            };


            // 7. Save Order to DB
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // 8. Pass Order (not OrderDetails) to confirmation view
            return View("Confirmation", orderDetails);
        }

        #endregion


        #region Order Payment

        public async Task<IActionResult> PayOrder(int? id)
        {
            if (id == null) return NotFound();

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Entries)
                    .ThenInclude(e => e.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PayOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.IsPaid = true;
            order.PaidAt = DateTime.UtcNow;  // if you add this field
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(History));
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Entries)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(History));
        }

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