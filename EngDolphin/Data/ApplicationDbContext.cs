using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EngDolphin.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Comment> UserComments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure StudentId as FK for StudentAddress
            modelBuilder.Entity<Comment>()
                .HasOne(o => o.AppUser)
                .WithMany(s => s.Comments);
        }
    }
}
