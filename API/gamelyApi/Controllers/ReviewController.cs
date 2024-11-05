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
    public class ReviewController : ControllerBase
    {
        private readonly GamelyDbContext _context;

        public ReviewController(GamelyDbContext context)
        {
            _context = context;
        }

        // CREATE: POST /api/reviews
        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview([FromBody] Review review)
        {
            review.Id = Guid.NewGuid();
            review.CreatedAt = DateTime.UtcNow;

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }

        // READ: GET /api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.Game) // Include the associated Game
                .ToListAsync();

            return Ok(reviews);
        }

        // READ: GET /api/reviews/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Review>> GetReviewById(Guid id)
        {
            var review = await _context.Reviews
                .Include(r => r.Game)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // UPDATE: PUT /api/reviews/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateReview(Guid id, [FromBody] Review updatedReview)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            // Update review properties
            review.Rating = updatedReview.Rating;
            review.Content = updatedReview.Content;
            review.Reviewer = updatedReview.Reviewer;
            review.GameId = updatedReview.GameId;
            review.CreatedAt = review.CreatedAt; // Preserve original creation date

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: DELETE /api/reviews/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteReview(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
