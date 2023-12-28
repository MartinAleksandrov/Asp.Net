namespace Forum.Data.Configurations
{
    using Forum.Data.Models;
    using Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PostEntityConfigurations : IEntityTypeConfiguration<Post>
    {
        private readonly PostSeeder postSeeder;

        public PostEntityConfigurations()
        {
            postSeeder = new PostSeeder();
        }   
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(postSeeder.GenaratePosts());
        }
    }
}
