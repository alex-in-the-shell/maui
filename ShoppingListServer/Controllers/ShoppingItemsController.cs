using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListServer.Data;
using ShoppingListServer.Models;

namespace ShoppingListServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingItemsController : ControllerBase
    {
        private readonly ShoppingListContext _context;

        public ShoppingItemsController(ShoppingListContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingItem>>> GetShoppingItems()
        {
            return await _context.ShoppingItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingItem>> GetShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);

            if (shoppingItem == null)
            {
                return NotFound();
            }

            return shoppingItem;
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingItem>> CreateShoppingItem(ShoppingItem shoppingItem)
        {
            shoppingItem.CreatedAt = DateTime.UtcNow;
            _context.ShoppingItems.Add(shoppingItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShoppingItem), new { id = shoppingItem.Id }, shoppingItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingItem(int id, ShoppingItem shoppingItem)
        {
            if (id != shoppingItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(shoppingItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingItem(int id)
        {
            var shoppingItem = await _context.ShoppingItems.FindAsync(id);
            if (shoppingItem == null)
            {
                return NotFound();
            }

            _context.ShoppingItems.Remove(shoppingItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
