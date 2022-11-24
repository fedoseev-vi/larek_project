using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using larek_catalogue.Data;

namespace larek_catalogue.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public ProductsApiController(CatalogueContext context)
        {
            _context = context;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("NamePart/{name_part}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByNamePart(string name_part)
        {
            var product = await _context.Products.Where(p => p.product_name.Contains(name_part)).ToListAsync();

            if (!_context.Products.Where(p => p.product_name.Contains(name_part)).Any())
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("CategoryId/{cat_id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(int cat_id)
        {
            var product = await _context.Products.Where(p => p.category_id == cat_id).ToListAsync();

            if (!_context.Products.Where(p => p.category_id == cat_id).Any())
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("BrandId/{bra_id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByBrand(int bra_id)
        {
            var product = await _context.Products.Where(p => p.brand_id == bra_id).ToListAsync();

            if (!_context.Products.Where(p => p.brand_id == bra_id).Any())
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/ProductsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.product_id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.product_id }, product);
        }

        // DELETE: api/ProductsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.product_id == id);
        }
    }
}
