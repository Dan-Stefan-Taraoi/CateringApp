using CateringApp.Data;
using CateringApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Controllers
{
    [ApiController]
    [Route("api/Inventory")]
    public class InventoryController : Controller
    {
        private readonly MyAppContext _context;

        public InventoryController(MyAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Index

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items
                .Include(i => i.Category)
                .ToListAsync();

            var grouped = items
                .GroupBy(i => i.Category?.Name ?? "Uncategorized")
                .ToDictionary(g => g.Key, g => g.ToList());

            return View(grouped);
        }

        #endregion

        #region Create

        public IActionResult Create()
        {
            PopulateViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateViewData();
            return View(item);
        }

        #endregion

        #region Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return NotFound();

            PopulateViewData();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Item item)
        {
            if (id != item.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateViewData();
            return View(item);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null) return NotFound();

            return View(item);
        }

        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        private void PopulateViewData()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        }
    }
}