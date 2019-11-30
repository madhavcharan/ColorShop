using AutoMapper;
using ColorShop.Data.Entities;
using ColorShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorShop.Controllers
{
    [Route("api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IPaintingRepository repository;
        private readonly IMapper mapper;
        public OrderItemsController(IPaintingRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;

        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = repository.GetOrderById(orderId);
            if (order != null)
            {
                return Ok(mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }
            else
            {
                return NotFound();
            }

        }

        /*
         * Returns the OrderItem that corresponds to the orderId and OrderItemid that was specified in the URL
         */
        [HttpGet("{id}")]

        public IActionResult Get(int orderId, int id)
        {
            var order = repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return Ok(mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
            }
            
                return NotFound();

        }
    }
}
