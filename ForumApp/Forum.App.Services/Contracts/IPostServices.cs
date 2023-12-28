namespace Forum.Services.Contracts
{
    using Forum.App.ViewModels.Post;
    public interface IPostServices
    {
        Task<IEnumerable<PostViewModel>> ListAllPosts();

        Task AddPostAsync(PostFormModel formModel);

        Task<PostFormModel> EditPost(string id);

        Task EditByIdAsync(string id, PostFormModel model);
    }
}