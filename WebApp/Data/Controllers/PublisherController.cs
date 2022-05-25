using Microsoft.AspNetCore.Mvc;
using WebApp.Data.DTOs;
using WebApp.Data.Models;
using WebApp.Data.Services;

namespace WebApp.Data.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherService _context;
        public PublisherController(PublisherService context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublisher()
        {
            return await _context.GetPublishers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
            var publisher = await _context.GetPublisher(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return publisher;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Publisher>> PutPublisher(int id, [FromBody] PublisherDTO publisher)
        {
            var result = await _context.UpdatePublisher(id, publisher);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher([FromBody] PublisherDTO publisher)
        {
            var result = await _context.AddPublisher(publisher);
            if (result == null)
            {
                BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var author = await _context.DeletePublisher(id);
            if (author)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpGet("incomplete")]
        public async Task<ActionResult<IEnumerable<IncompletePublisherDTO>>> GetPublisherIncomplete()
        {
            return await _context.GetPublishersIncomplete();
        }
    }
}
