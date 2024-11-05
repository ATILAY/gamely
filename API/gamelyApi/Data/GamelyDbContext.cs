using gamelyApi.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace gamelyApi.Data
{
    public class GamelyDbContext : DbContext
    {
        public GamelyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Game> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Platform> Platforms { get; set; }


        // Optional: Configure relationships and schema using Fluent API in OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example of defining a many-to-many relationship between Games and Platforms
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Platforms)
                .WithMany(p => p.Games);

            // Define foreign key relationships
            modelBuilder.Entity<Game>()
                .HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.PublisherId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Game)
                .WithMany(g => g.Reviews)
                .HasForeignKey(r => r.GameId);
        }

    }
}
