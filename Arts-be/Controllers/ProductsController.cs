using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arts_be.Models;
using Arts_be.Models.DTO;
using Azure;

namespace Arts_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly EProjectContext _context;

        public ProductsController(EProjectContext context)
        {
            _context = context;
        }

        // GET: api/ProductsDTO
        [HttpGet("productDTO")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct()
        {

            try
            {
                var result = await _context.Products
                    .Join(
                        _context.ProductImages,
                        product => product.ProductId,
                        image => image.Product_id,
                        (product, image) => new
                        {
                            product,
                            image
                        }
                    )
                    .Join(
                        _context.Categories,
                        combined => combined.product.ProductCategoryId,
                        category => category.CategoryId,
                        (combined, category) => new ProductDTO
                        {
                            productId = combined.product.ProductId,
                            Product_code = combined.product.ProductCode,
                            Description = combined.product.Description,
                            Price = combined.product.Price,
                            Title = combined.product.Title,
                            Quantity = (int)combined.product.Quantity,
                            Weight = combined.product.Weight,
                            Tag = combined.product.Tags,
                            Availability = combined.product.Availability,
                            Sale = combined.product.Sale,
                            SKU = combined.product.SKU,
                            Brands = combined.product.Brands,
                            CommentsBrands = combined.product.CommentBrands,
                            // Add other properties from Product entity as needed
                            path = combined.image.Path,

                            // Properties from Category entity
                            CategoryId = category.CategoryId,
                            CategoryName = category.NameCategory,
                            // Add other properties from Category entity as needed
                        }
                    )
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            /*       if (_context.Products == null)
                   {
                       return NotFound();
                   }
                   return await _context.Products.ToListAsync();*/
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        // GET: api/Products/5
        [HttpGet("productDTO/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductDTO(int id)
        {
            try
            {
                var result = await _context.Products
                    .Where(product => product.ProductId == id)
                    .Join(
                        _context.ProductImages,
                        product => product.ProductId,
                        image => image.Product_id,
                        (product, image) => new
                        {
                            product,
                            image
                        }
                    )
                    .Join(
                        _context.Categories,
                        combined => combined.product.ProductCategoryId,
                        category => category.CategoryId,
                        (combined, category) => new ProductDTO
                        {
                            productId = combined.product.ProductId,
                            Product_code = combined.product.ProductCode,
                            Description = combined.product.Description,
                            Price = combined.product.Price,
                            Title = combined.product.Title,
                            Quantity = (int)combined.product.Quantity,
                            Weight = combined.product.Weight,
                            Tag = combined.product.Tags,
                            Availability = combined.product.Availability,
                            Sale = combined.product.Sale,
                            SKU = combined.product.SKU,
                            Brands = combined.product.Brands,
                            CommentsBrands = combined.product.CommentBrands,
                            // Add other properties from Product entity as needed
                            path = combined.image.Path,

                            // Properties from Category entity
                            CategoryId = category.CategoryId,
                            CategoryName = category.NameCategory,
                            // Add other properties from Category entity as needed
                        }
                    )
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'EProjectContext.Products'  is null.");
          }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
