namespace Forum.Services
{
    using Forum.App.ViewModels.Post;
    using Forum.Data;
    using Forum.Data.Models;
    using Forum.Services.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class PostService : IPostServices
    {
        private readonly ForumDbContext dbContext;

        public PostService(ForumDbContext context)
        {
            this.dbContext = context;
        }
        public async Task<IEnumerable<PostViewModel>> ListAllPosts()
        {
            var allPosts = await dbContext
                .Posts
                .Select(p => new PostViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Content = p.Content
                })
                .ToArrayAsync();

            return allPosts;
        }

        public async Task AddPostAsync(PostFormModel formModel)
        {
            var postToAdd = new Post
            {
                Title = formModel.Title,
                Content = formModel.Content
            };

            await dbContext.Posts.AddAsync(postToAdd);
            await dbContext.SaveChangesAsync();
        }

        public async Task<PostFormModel> EditPost(string Id)
        {
            Post? postToEdit = await this.dbContext
                 .Posts
                 .FirstOrDefaultAsync(p => p.Id.ToString() == Id);

            return new PostFormModel()
            {
                Title = postToEdit.Title,
                Content = postToEdit.Content
            };
        }

        public async Task EditByIdAsync(string Id,PostFormModel model)
        {
            Post? postToEdit = await this.dbContext
                .Posts
                .FirstOrDefaultAsync(p => p.Id.ToString() == Id);

            postToEdit.Title = model.Title;
            postToEdit.Content = model.Content;

            await this.dbContext.SaveChangesAsync();
        }
    }
}