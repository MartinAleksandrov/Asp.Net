namespace Homies.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Homies.Data.DataModels;
    public class HomiesDbContext : IdentityDbContext
    {
        public DbSet<Type> Types { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<EventsParticipants> EventParticipants { get; set; } = null!;


        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventsParticipants>()
                .HasKey(e => new { e.EventId, e.HelperId });

            modelBuilder.Entity<EventsParticipants>()
               .HasOne(e => e.Event)
               .WithMany(e => e.EventsParticipants)
               .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder
                .Entity<Type>()
                .HasData(new Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Type()
                {
                    Id = 4,
                    Name = "Work"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}