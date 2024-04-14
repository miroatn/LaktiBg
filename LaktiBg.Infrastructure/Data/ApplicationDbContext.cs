using LaktiBg.Infrastructure.Data.Models;
using LaktiBg.Infrastructure.Data.SeedDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaktiBg.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        public DbSet<EventTypeConnection> EventTypeConnections { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UsersEvents>()
                .HasKey(e => new {e.EventId, e.UserId});

            builder.Entity<UserFriends>()
                .HasOne(u => u.User)
                .WithMany(u => u.Friends)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EventTypeConnection>()
                .HasKey(etc => new {etc.EventId, etc.EventTypeId});

            builder.Entity<Image>()
                .Property(i => i.PlaceId)
                .IsRequired(false);

            builder.Entity<Image>()
                .Property(i => i.UserId)
                .IsRequired(false);

            builder.Entity<Image>()
                .Property(i => i.EventId)
                .IsRequired(false);


            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

            builder.ApplyConfiguration(new EventTypeConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PlaceConfiguration());
            builder.ApplyConfiguration(new EventConfiguration());
            builder.ApplyConfiguration(new EventConnectionConfiguration());
            builder.ApplyConfiguration(new UsersEventsConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());

            base.OnModelCreating(builder);

        }
    }

    internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

        }
    }
}