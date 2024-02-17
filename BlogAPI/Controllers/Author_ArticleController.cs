using BlogAPI.Database;
using BlogAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorArticleController : ControllerBase
    {
        private readonly BlogDb _context;

        public AuthorArticleController(BlogDb context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorArticle>>> GetAll()
        {
            return await _context.AuthorArticles.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorArticle>> GetById(int id)
        {
            var authorArticle = await _context.AuthorArticles.FindAsync(id);
            if (authorArticle == null)
                return NotFound();
            return authorArticle;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorArticle>> Create(AuthorArticle authorArticle)
        {
            _context.AuthorArticles.Add(authorArticle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = authorArticle.Id }, authorArticle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuthorArticle authorArticle)
        {
            if (id != authorArticle.Id)
                return BadRequest();

            _context.Entry(authorArticle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorArticleExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var authorArticle = await _context.AuthorArticles.FindAsync(id);
            if (authorArticle == null)
                return NotFound();

            _context.AuthorArticles.Remove(authorArticle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorArticleExists(int id)
        {
            return _context.AuthorArticles.Any(e => e.Id == id);
        }
    }
}
