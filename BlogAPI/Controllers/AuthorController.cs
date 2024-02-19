using BlogAPI.Database;
using BlogAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase
    {
        private readonly BlogDb _context;

        public AuthorController(BlogDb context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();
            return author;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> Create(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Author author)
        {
            if (id != author.AuthorId)
                return BadRequest();

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return NotFound();

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
