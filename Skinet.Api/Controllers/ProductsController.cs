using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skinet.Infrastructure.Data;
using Skinet.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skinet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("GetProduct{id}")]
        public async Task<ActionResult<Product>> GetProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return Ok(product);
        }
    }
}
