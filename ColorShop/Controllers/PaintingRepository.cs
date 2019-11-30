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


        public PaintingRepository(ColorShopContext context)
        {
            this.context = context;
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }

        /*
         * Get All Orders including the OrderItems and the product that the OrderItem they correspond to.
         */
        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders
                .Include(o =>o.Items)
                .ThenInclude(item =>item.Product)
                .ToList(); ;
        }

        /*
         * Get all products in the shop ordered by the title
         */
        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.OrderBy(p => p.Title).ToList();
        }

        /*
         * Get Order by Id including the OrderItems and the product that the OrderItem they correspond to.
         */
        public Order GetOrderById(int id)
        {
            return context.Orders
                .Include(order => order.Items)
                .ThenInclude(item => item.Product)
                .Where(order => order.Id == id)
                .FirstOrDefault();
        }

        /*
         * Get all products for a given category
         */
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
