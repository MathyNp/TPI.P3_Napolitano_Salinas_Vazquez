using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPI_NapolitanoSalinasVazquez_P3.Data;
using TPI_NapolitanoSalinasVazquez_P3.Models;

namespace TPI_NapolitanoSalinasVazquez_P3.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly TPI_NapolitanoSalinasVazquez_P3Context _context;

        public ProductsController(TPI_NapolitanoSalinasVazquez_P3Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Product.ToListAsync();
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.productID }, product);
        }
    }
}
