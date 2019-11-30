using System.Collections.Generic;
using ColorShop.Data.Entities;

namespace ColorShop.Controllers
{
    public interface IPaintingRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        void AddEntity(object model);
    }
}