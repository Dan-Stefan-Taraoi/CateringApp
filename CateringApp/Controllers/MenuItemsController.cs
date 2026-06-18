using CateringApp.Data;
using CateringApp.Models;
using CateringApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly MyAppContext _context;

        public MenuItemsController(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            var menuItems = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                    .ThenInclude(ki => ki.Item)
                .ToListAsync();

            return View(menuItems);
        }

        #endregion

        #region Create

        // GET : MenuItems/Create
        public async Task<IActionResult> Create()
        {
            PopulateViewData();

            // Pass all inventory items so staff can pick ingredients
            ViewBag.AvailableItems = await _context.Items
                .Include(i => i.Category)
                .OrderBy(i => i.Name)
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            MenuItem menuItem,
            List<int> selectedItemIds,
            IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                _context.MenuItems.Add(menuItem);
                await _context.SaveChangesAsync();

                // Save KitchenItems
                foreach (var itemId in selectedItemIds)
                {
                    var qtyKey = $"quantities_{itemId}";
                    var qty = double.TryParse(
                        form[qtyKey],
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var parsed) ? parsed : 0;

                    if (qty > 0)
                    {
                        _context.KitchenItems.Add(new KitchenItem
                        {
                            MenuItemId = menuItem.Id,
                            ItemId = itemId,
                            QuantityRequired = qty
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateViewData();
            return View(menuItem);
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var menuItem = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                    .ThenInclude(ki => ki.Item)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (menuItem == null) return NotFound();

            ViewBag.AvailableItems = await _context.Items
                .Include(i => i.Category)
                .OrderBy(i => i.Name)
                .ToListAsync();

            // Pass existing quantities so the form can pre-fill them
            ViewBag.ExistingKitchenItems = menuItem.KitchenItems?
                .ToDictionary(ki => ki.ItemId, ki => ki.QuantityRequired)
                ?? new Dictionary<int, double>();

            PopulateViewData();
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            MenuItem menuItem,
            List<int> selectedItemIds,
            IFormCollection form)
        {
            if (id != menuItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(menuItem);

                // Remove existing KitchenItems and replace
                var existing = _context.KitchenItems
                    .Where(ki => ki.MenuItemId == id);
                _context.KitchenItems.RemoveRange(existing);

                foreach (var itemId in selectedItemIds)
                {
                    var qtyKey = $"quantities_{itemId}";
                    var qty = double.TryParse(
                        form[qtyKey],
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var parsed) ? parsed : 0;

                    if (qty > 0)
                    {
                        _context.KitchenItems.Add(new KitchenItem
                        {
                            MenuItemId = menuItem.Id,
                            ItemId = itemId,
                            QuantityRequired = qty
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateViewData();
            return View(menuItem);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var menuItem = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                    .ThenInclude(ki => ki.Item)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (menuItem == null) return NotFound();

            return View(menuItem);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItems
                .Include(m => m.KitchenItems!)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (menuItem != null)
            {
                // Remove KitchenItems first to avoid FK constraint violation
                if (menuItem.KitchenItems != null)
                    _context.KitchenItems.RemoveRange(menuItem.KitchenItems);

                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        private void PopulateViewData()
        {
            ViewBag.CookingMethods = Enum.GetValues<CookingMethod>()
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.ToString()
                }).ToList();
        }
    }
}