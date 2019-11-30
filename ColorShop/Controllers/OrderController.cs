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
    public class OrderController : Controller
    {
        private readonly IPaintingRepository repository;
        private readonly IMapper mapper;

        public OrderController(IPaintingRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(repository.GetAllOrders()));
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get orders : {ex}");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {

            try
            {
                var order = repository.GetOrderById(id);
                if (order != null)
                    return Ok(mapper.Map<Order, OrderViewModel>(order));
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get order with id: {ex}");
            }

        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = mapper.Map<OrderViewModel, Order>(model);
                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    repository.AddEntity(model);
                    if (repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }

                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Failed to save order with id: {ex}");
            }

            return BadRequest($"Failed to save order with id: {model.OrderId}");
        }
    }
}
