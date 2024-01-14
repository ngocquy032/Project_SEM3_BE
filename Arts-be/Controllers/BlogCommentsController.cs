using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arts_be.Models;

namespace Arts_be.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentsController : ControllerBase
    {
        private readonly EProjectContext _context;

        public BlogCommentsController(EProjectContext context)
        {
            _context = context;
        }

        // GET: api/BlogComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogComment>>> GetBlogComments()
        {
          if (_context.BlogComments == null)
          {
              return NotFound();
          }
            return await _context.BlogComments.ToListAsync();
        }

        // GET: api/BlogComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogComment>> GetBlogComment(int id)
        {
          if (_context.BlogComments == null)
          {
              return NotFound();
          }
            var blogComment = await _context.BlogComments.FindAsync(id);

            if (blogComment == null)
            {
                return NotFound();
            }

            return blogComment;
        }

        // PUT: api/BlogComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogComment(int id, BlogComment blogComment)
        {
            if (id != blogComment.BlogCommentId)
            {
                return BadRequest();
            }

            _context.Entry(blogComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogCommentExists(id))
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

        // POST: api/BlogComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogComment>> PostBlogComment(BlogComment blogComment)
        {
          if (_context.BlogComments == null)
          {
              return Problem("Entity set 'EProjectContext.BlogComments'  is null.");
          }
            _context.BlogComments.Add(blogComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogComment", new { id = blogComment.BlogCommentId }, blogComment);
        }

        // DELETE: api/BlogComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogComment(int id)
        {
            if (_context.BlogComments == null)
            {
                return NotFound();
            }
            var blogComment = await _context.BlogComments.FindAsync(id);
            if (blogComment == null)
            {
                return NotFound();
            }

            _context.BlogComments.Remove(blogComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogCommentExists(int id)
        {
            return (_context.BlogComments?.Any(e => e.BlogCommentId == id)).GetValueOrDefault();
        }
    }
}
