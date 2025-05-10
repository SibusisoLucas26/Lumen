using Microsoft.EntityFrameworkCore;
using ChatApp.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace ChatApp.Data
{
    /// <summary>
    /// Database context for the chat application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string> {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the messages in the database.
        /// </summary>
        public DbSet<Message> Messages { get; set; }

        /// <summary>
        /// Configures the model for the database.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Message entity
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(m => m.Id); // Set primary key
                entity.Property(m => m.Content).IsRequired().HasMaxLength(1000); // Limit content length
                entity.Property(m => m.Timestamp).IsRequired(); // Ensure timestamp is required
            });
        }
    }
}