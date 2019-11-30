using ColorShop.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ColorShop.Data
{
    public class ColorShopSeeder
    {
        private readonly ColorShopContext context;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<StoreUser> userManager;

        public ColorShopSeeder(ColorShopContext context, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            this.context = context;
            this.hosting = hosting;
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            context.Database.EnsureCreated();
            StoreUser user = await userManager.FindByEmailAsync("charan.madhav@colorshop.com");
            if(user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Madhav",
                    LastName = "Charan",
                    Email = "charan.madhav@colorshop.com",
                    UserName = "charan.madhav@colorshop.com"
                };
                var result = await userManager.CreateAsync(user, "Pa$$w0rd!");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Cannot create new user in seeder");
                }
            }
            if(!context.Products.Any())
            {
                var filepath = Path.Combine(hosting.ContentRootPath,"Data/paintings.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                context.Products.AddRange(products);
                var order = context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if(order !=null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price

                        }
                    };
                }
                context.SaveChanges();
            }
        }
    }
}
