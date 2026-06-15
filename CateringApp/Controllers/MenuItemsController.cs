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

        public IActionResult Create()
        {
            PopulateViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _context.MenuItems.Add(menuItem);
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

            PopulateViewData();
            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(menuItem);
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