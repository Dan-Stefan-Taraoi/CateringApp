using CateringApp.Data;
using CateringApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Controllers
{
    public class InventoryController : Controller
    {
        private readonly MyAppContext _context;

        public InventoryController(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region CRUD for InventoryItems (Ingredients and HardwareItems)

        #region Index

        // GET: InventoryItems - This action will display the inventory items overview page
        public async Task<IActionResult> Index()
        {
            var inventoryItems = await _context.InventoryItems
                .Include(i => i.Category)
                .Include(i => i.ItemClients!)
                    .ThenInclude(ic => ic.Client)
                .ToListAsync();

            // loading serial numbers for hardware items
            foreach (var item in inventoryItems.OfType<HardwareItem>())
            {
                await _context.Entry(item)
                    .Reference(h => h.SerialNumber)
                    .LoadAsync();
            }

            return View(inventoryItems);
        }

        #endregion Index

        #region Create

        // GET: Inventory/CreateIngredient
        public IActionResult CreateIngredient()
        {
            PopulateViewData();
            return View();
        }

        // POST: Inventory/CreateIngredient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIngredient(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                _context.Ingredients.Add(ingredient);  // more explicit than _context.Add()
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate ViewData if validation fails — form needs it to re-render
            PopulateViewData();
            return View(ingredient);
        }

        // GET: Inventory/CreateHardware
        public IActionResult CreateHardware()
        {
            PopulateViewData();
            return View();
        }

        // POST: Inventory/CreateHardware
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHardware(HardwareItem hardwareItem, string? SerialNumberName)
        {
            if (ModelState.IsValid)
            {
                _context.HardwareItems.Add(hardwareItem);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(SerialNumberName))
                {
                    var serial = new SerialNumber
                    {
                        Name = SerialNumberName,
                        HardwareItemId = hardwareItem.Id
                    };
                    _context.SerialNumbers.Add(serial);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            PopulateViewData();
            return View(hardwareItem);
        }

        #endregion Create

        #region Edit

        // GET: Inventory/EditIngredient
        public async Task<IActionResult> EditIngredient(int? id)
        {
            if (id == null) return NotFound();

            var ingredient = await _context.Ingredients
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null) return NotFound();

            PopulateViewData();
            return View(ingredient);
        }

        // POST: Inventory/EditIngredient
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(ingredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateViewData();
            return View(ingredient);
        }

        // GET: Inventory/EditHardware
        /*FirstOrDefaultAsync with Include(h => h.SerialNumber) instead of FindAsync — FindAsync doesn't support Include
         * , so the serial number would be null when the edit form loads. You need FirstOrDefaultAsync here to eagerly load it.*/
        public async Task<IActionResult> EditHardware(int? id)
        {
            if (id == null) return NotFound();

            var itemHardware = await _context.HardwareItems
                .Include(h => h.SerialNumber)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (itemHardware == null) return NotFound();

            PopulateViewData();
            return View(itemHardware);
        }

        // POST: Inventory/EditHardware
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHardware(int id, HardwareItem hardwareItem, string? SerialNumberName)
        {
            if (id != hardwareItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(hardwareItem);

                // Handle serial number update
                var existing = await _context.SerialNumbers
                    .FirstOrDefaultAsync(s => s.HardwareItemId == id);

                if (!string.IsNullOrWhiteSpace(SerialNumberName))
                {
                    if (existing != null)
                        existing.Name = SerialNumberName; // update
                    else
                        _context.SerialNumbers.Add(new SerialNumber
                        {
                            Name = SerialNumberName,
                            HardwareItemId = hardwareItem.Id
                        }); // create new
                }
                else if (existing != null)
                {
                    _context.SerialNumbers.Remove(existing); // cleared — remove it
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateViewData();
            return View(hardwareItem);
        }

        #endregion Edit

        #region Delete

        // GET: Inventory/DeleteIngredient
        public async Task<IActionResult> DeleteIngredient(int? id)
        {
            if (id == null) return NotFound();

            var ingredient = await _context.Ingredients
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ingredient == null) return NotFound();

            return View(ingredient);
        }

        // POST: Inventory/DeleteIngredient
        [HttpPost, ActionName(nameof(DeleteIngredient))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteIngredientConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Inventory/DeleteHardware
        public async Task<IActionResult> DeleteHardware(int? id)
        {
            if (id == null) return NotFound();

            var itemHardware = await _context.HardwareItems
                .Include(h => h.Category)
                .Include(h => h.SerialNumber)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (itemHardware == null) return NotFound();

            return View(itemHardware);
        }

        // POST: Inventory/DeleteHardware
        [HttpPost, ActionName(nameof(DeleteHardware))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteHardwareConfirm(int id)
        {
            var itemHardware = await _context.HardwareItems
                .Include(h => h.SerialNumber)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (itemHardware != null)
            {
                // Remove SerialNumber first to avoid FK constraint violation
                // Needed removel giving the 1-to-1 relationship between HardwareItem and SerialNumber
                if (itemHardware.SerialNumber != null)
                    _context.SerialNumbers.Remove(itemHardware.SerialNumber);

                _context.HardwareItems.Remove(itemHardware);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion Delete

        #endregion CRUD for InventoryItems (Ingredients and HardwareItems)

        private void PopulateViewData()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        }
    }
}
