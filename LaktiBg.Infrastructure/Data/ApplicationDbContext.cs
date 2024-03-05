using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaktiBg.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<EventType> EventTypes { get; set; } = null!;

        public DbSet<Image> Images { get; set; } = null!;

        public DbSet<Place> Places { get; set; } = null!;

        public DbSet<UsersEvents> UsersEvents { get; set; } = null!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<UsersEvents>()
                .HasKey(e => new {e.EventId, e.UserId});

            builder.Entity<UserFriends>()
                .HasOne(u => u.User)
                .WithMany(u => u.Friends)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());


        }
    }

    internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

        }
    }
}