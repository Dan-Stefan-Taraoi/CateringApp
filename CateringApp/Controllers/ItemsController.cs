using CateringApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CateringApp.Controllers
{
    public class ItemsController : Controller
    {
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
