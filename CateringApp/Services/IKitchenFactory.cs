using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public interface IKitchenFactory
    {
        IDish CreateDish(MenuItem menuItem, string? location = null);
    }
}
