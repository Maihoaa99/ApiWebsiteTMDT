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
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _categoryContext;

        public CategoryController(DatabaseContext context)
        {
            _categoryContext = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {
            return _categoryContext.Categorys;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _categoryContext.Categorys.FindAsync(id);
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            if (!_categoryContext.Categorys.Any(c => c.code == category.code))
            {
                _categoryContext.Categorys.Add(category);
                await _categoryContext.SaveChangesAsync();
                return CreatedAtAction(nameof(PostCategory), new { id = category.id }, category);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            _categoryContext.Entry(category).State = EntityState.Modified;
            try
            {
                await _categoryContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryAvailable(id))
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

        private bool CategoryAvailable(Guid id)
        {
            return (_categoryContext.Categorys?.Any(x => x.id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryContext.Categorys.Include(c => c.products).FirstOrDefaultAsync(c => c.id == id);
            if (category?.products.Count == 0)
            {
                _categoryContext.Categorys.Remove(category);
                await _categoryContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
