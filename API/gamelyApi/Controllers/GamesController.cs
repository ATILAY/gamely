using gamelyApi.Data;
using gamelyApi.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gamelyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamelyDbContext _context;

        public GamesController(GamelyDbContext context)
        {
            _context = context;
        }

        // CREATE: POST /api/games
        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame([FromBody] Game game)
        {
            game.Id = Guid.NewGuid();
            game.CreatedAt = DateTime.UtcNow;

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
        }

        // READ: GET /api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            var games = await _context.Games
                .Include(g => g.Publisher) // Include Publisher details
                .Include(g => g.Platforms) // Include Platforms list
                .Include(g => g.Reviews)   // Include Reviews list
                .ToListAsync();

            return Ok(games);
        }

        // READ: GET /api/games/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Game>> GetGameById(Guid id)
        {
            var game = await _context.Games
                .Include(g => g.Publisher)
                .Include(g => g.Platforms)
                .Include(g => g.Reviews)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        // UPDATE: PUT /api/games/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateGame(Guid id, [FromBody] Game updatedGame)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            // Update properties
            game.Title = updatedGame.Title;
            game.Description = updatedGame.Description;
            game.ReleaseDate = updatedGame.ReleaseDate;
            game.PublisherId = updatedGame.PublisherId;
            game.Genre = updatedGame.Genre;
            game.AverageRating = updatedGame.AverageRating;
            game.CoverImageUrl = updatedGame.CoverImageUrl;
            game.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: DELETE /api/games/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteGame(Guid id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
