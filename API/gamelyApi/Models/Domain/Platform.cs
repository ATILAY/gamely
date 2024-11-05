using gamelyApi.Models.Domain;
using System;
using System.Collections.Generic;

namespace gamelyApi.Models.Domain
{
    public class Platform
    {
        // Unique identifier for each platform
        public Guid Id { get; set; } = Guid.NewGuid();

        // Name of the platform (e.g., PC, PlayStation) (required)
        public required string Name { get; set; }

        // Collection of games available on this platform
        public List<Game> Games { get; set; } = new List<Game>();

        // Date the platform entry was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Date the platform entry was last updated
        public DateTime? UpdatedAt { get; set; }
    }
}
