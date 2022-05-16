using Microsoft.AspNetCore.Mvc;
using WebApp.Data.DTOs;
using WebApp.Data.Models;
using WebApp.Data.Services;

namespace WebApp.Data.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _context;
        public AuthorController(AuthorService context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            return await _context.GetAuthors();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }
            return author;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> PutAuthor(int id, [FromBody] Author author)
        {
            var result = await _context.UpdateAuthor(id, author);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor([FromBody] AuthorDTO author)
        {
            var result = await _context.AddAuthor(author);
            if (result == null)
            {
                BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.DeleteAuthor(id);
            if (author)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<IncompleteAuthorDTO>>> GetAuthorIncomplete()
        {
            return await _context.GetAuthorsIncomplete();
        }


    }
}