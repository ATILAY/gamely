using gamelyApi.Data;
using gamelyApi.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gamelyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly GamelyDbContext _context;

        public PublisherController(GamelyDbContext context)
        {
            _context = context;
        }

        // CREATE: POST /api/publishers
        [HttpPost]
        public async Task<ActionResult<Publisher>> CreatePublisher([FromBody] Publisher publisher)
        {
            // Set default values if needed
            publisher.Id = Guid.NewGuid();
            publisher.CreatedAt = DateTime.UtcNow;

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublisherById), new { id = publisher.Id }, publisher);
        }

        // READ: GET /api/publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers()
        {
            var publishers = await _context.Publishers
                .Include(p => p.Games) // Include Games list
                .ToListAsync();

            return Ok(publishers);
        }

        // READ: GET /api/publishers/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Publisher>> GetPublisherById(Guid id)
        {
            var publisher = await _context.Publishers
                .Include(p => p.Games)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        // UPDATE: PUT /api/publishers/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdatePublisher(Guid id, [FromBody] Publisher updatedPublisher)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            // Update properties
            publisher.Name = updatedPublisher.Name;
            publisher.Country = updatedPublisher.Country;
            publisher.WebsiteUrl = updatedPublisher.WebsiteUrl;
            publisher.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: DELETE /api/publishers/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletePublisher(Guid id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
