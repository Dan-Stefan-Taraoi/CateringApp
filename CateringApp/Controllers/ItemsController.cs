using CateringApp.Data;
using CateringApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CateringApp.Controllers
{

    /// <summary>
    /// The ItemsController is responsible for handling requests related to items in the catering application.<br/>
    /// It provides actions for displaying an overview of items and editing specific items based on their ID.
    /// </summary>
    public class ItemsController : Controller
    {
        private readonly MyAppContext _myAppContext;

        /// <summary>
        /// Initializes a new instance of the ItemsController class with the specified MyAppContext.<br/>
        /// </summary>
        /// <param name="myAppContext"></param>
        public ItemsController(MyAppContext myAppContext)
        {
            _myAppContext = myAppContext;
        }

        public async Task<IActionResult > Index()
        {
            var items = await _myAppContext.Items.ToListAsync();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Description, Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _myAppContext.Items.Add(item);
                await _myAppContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _myAppContext.Items.FirstOrDefaultAsync(m => m.Id == id);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Description, Price")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _myAppContext.Update(item);
                await _myAppContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _myAppContext.Items.FirstOrDefaultAsync(m => m.Id == id);
            return View(item);
        }

        [HttpPost, ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _myAppContext.Items.FindAsync(id);
            if (item != null)
            {
                _myAppContext.Items.Remove(item);
                await _myAppContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Placeholder of manual parameter fill.<br/>
        /// </summary>
        /// <returns></returns>
        public IActionResult OverView()
        {
            var item = new Item() { Name = "Keyboard" };
            return View(item);
        }
    }
}
