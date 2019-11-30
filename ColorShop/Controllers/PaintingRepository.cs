using ColorShop.Data;
using ColorShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorShop.Controllers
{
    public class PaintingRepository : IPaintingRepository
    {
        private readonly ColorShopContext context;

        public PaintingRepository()
        {
        }

        public PaintingRepository(ColorShopContext context)
        {
            this.context = context;
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders
                .Include(o =>o.Items)
                .ThenInclude(item =>item.Product)
                .ToList(); ;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.OrderBy(p => p.Title).ToList();
        }

        public Order GetOrderById(int id)
        {
            return context.Orders
                .Include(order => order.Items)
                .ThenInclude(item => item.Product)
                .Where(order => order.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return context.Products.Where(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }
    }
}
