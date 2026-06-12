using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public interface IKitchenFactory
    {
        public IDish CreateDish(MenuItem menuItem);
    }
}
