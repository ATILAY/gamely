using gamelyApi.Models.Domain;
using System;
using System.Collections.Generic;

namespace gamelyApi.Models.Domain
{
    public class Publisher
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }

        public string? Country { get; set; }

        // Website URL of the publisher
        public string? WebsiteUrl { get; set; }

        // Collection of games published by this publisher
        public List<Game> Games { get; set; } = new List<Game>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
