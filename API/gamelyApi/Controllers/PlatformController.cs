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
    public class PlatformsController : ControllerBase
    {
        private readonly GamelyDbContext _context;

        public PlatformsController(GamelyDbContext context)
        {
            _context = context;
        }

        // CREATE: POST /api/platforms
        [HttpPost]
        public async Task<ActionResult<Platform>> CreatePlatform([FromBody] Platform platform)
        {
            platform.Id = Guid.NewGuid();
            platform.CreatedAt = DateTime.UtcNow;

            _context.Platforms.Add(platform);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, platform);
        }

        // READ: GET /api/platforms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Platform>>> GetAllPlatforms()
        {
            var platforms = await _context.Platforms
                .Include(p => p.Games) // Include Games list
                .ToListAsync();

            return Ok(platforms);
        }

        // READ: GET /api/platforms/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Platform>> GetPlatformById(Guid id)
        {
            var platform = await _context.Platforms
                .Include(p => p.Games)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (platform == null)
            {
                return NotFound();
            }

            return Ok(platform);
        }

        // UPDATE: PUT /api/platforms/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdatePlatform(Guid id, [FromBody] Platform updatedPlatform)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }

            // Update properties
            platform.Name = updatedPlatform.Name;
            platform.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: DELETE /api/platforms/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeletePlatform(Guid id)
        {
            var platform = await _context.Platforms.FindAsync(id);
            if (platform == null)
            {
                return NotFound();
            }

            _context.Platforms.Remove(platform);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
