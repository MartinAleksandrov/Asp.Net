namespace Forum.Data
{
    using Forum.Data.Configurations;
    using Forum.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            :base(options) 
        {
                
        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityConfigurations());

            base.OnModelCreating(modelBuilder);
        }
    }
}
