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

        /// <summary>
        /// Placeholder of manual parameter fill.<br/>
        /// </summary>
        /// <returns></returns>
        public IActionResult OverView()
        {
            var item = new Item() { Name = "Keyboard" };
            return View(item);
        }

        /// <summary>
        /// Placeholder of automative parameter fill.<br/>
        /// Needs to be 'id'.
        /// </summary>
        /// <param name="id">Automatic parameter.</param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            return Content("Id is: " + id);
        }
    }
}
