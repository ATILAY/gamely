using Microsoft.AspNetCore.Mvc.ViewEngines;
using gamelyApi.Models.Domain;
using System;
using System.Collections.Generic;

namespace gamelyApi.Models.Domain
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        // Foreign key for Publisher
        public Guid? PublisherId { get; set; }

        // Navigation property for Publisher
        public Publisher? Publisher { get; set; }

        public List<Review>? Reviews { get; set; } = new List<Review>();

        // Collection of platforms the game is available on
        public List<Platform>? Platforms { get; set; } = new List<Platform>();

        public string? Genre { get; set; }

        public double? AverageRating { get; set; }

        public string? CoverImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
