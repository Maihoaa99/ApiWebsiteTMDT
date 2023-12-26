using Delivery.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _productContext;

        public ProductController(ProductContext context)
        {
            _productContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProduct()
        {
            return _productContext.Products;

        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetProduct))]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _productContext.Products.FindAsync(id);
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product, int code)
        {
            if (!_productContext.Products.Any(p => p.code == product.code))
            {
                _productContext.Products.Add(product);
                await _productContext.SaveChangesAsync();
                return CreatedAtAction(nameof(PostProduct), new { id = product.id }, product);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Product product, Guid id, int code)
        {
            var productt = await _productContext.Products.FindAsync(id);
            if (productt != null)
            {
                // delete -> update is_hidden = false
                productt.is_hidden = false;
                _productContext.Products.Update(productt);
                if(productt.code == product.code)
                {
                    // add khi code trung product cu
                    _productContext.Products.Add(product);
                    await _productContext.SaveChangesAsync();
                    return CreatedAtAction(nameof(UpdateProduct), new { id = product.id }, product);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            _productContext.Entry(product).State = EntityState.Modified;
            try
            {
                await _productContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool ProductAvailable(Guid id)
        {
            return (_productContext.Products?.Any(x => x.id == id)).GetValueOrDefault();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _productContext.Products.FindAsync(id);
            if (product != null)
            {
                product.is_hidden = false;
                _productContext.Products.Update(product);
                await _productContext.SaveChangesAsync();
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
