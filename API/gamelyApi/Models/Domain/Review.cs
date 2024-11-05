using gamelyApi.Models.Domain;
using System;

namespace gamelyApi.Models.Domain
{
    public class Review
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required double Rating { get; set; }

        // Text content of the review
        public string? Content { get; set; }

        // Reviewer name or ID (optional)
        public string? Reviewer { get; set; }

        // Foreign key for the game being reviewed
        public Guid GameId { get; set; }

        // Navigation property for Game
        public Game Game { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
