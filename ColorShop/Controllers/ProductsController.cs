using ColorShop.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorShop.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly IPaintingRepository repository;

        public ProductsController(IPaintingRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(repository.GetAllProducts());
            }
            catch(Exception ex)
            {
                return BadRequest($"Failed to get Products : {ex}");
            }

        }
    }
}
