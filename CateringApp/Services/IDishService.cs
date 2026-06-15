using CateringApp.Models;
using CateringApp.Models.Interfaces;

namespace CateringApp.Services
{
    public interface IDishService
    {
        IDish CreateDish(MenuItem menuItem);
        void PrepareOrder(OrderDetails order);
    }
}