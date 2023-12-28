using Forum.Data.Models;

namespace Forum.Data.Seeding
{
    public class PostSeeder
    {
        public Post[] GenaratePosts()
        {
            var posts = new List<Post>();

            posts.Add(new Post
            {
                Title = "FisrtPost",
                Content = "My First Post using ASP.NET"
            });

            posts.Add(new Post
            {
                Title = "SecondPost",
                Content = "My Second Post using ASP.NET"
            });

            posts.Add(new Post
            {
                Title = "ThirdPost",
                Content = "My Third Post using ASP.NET"
            });

            return posts.ToArray();
        }
    }
}
