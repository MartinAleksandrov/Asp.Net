namespace ForumApp.Controllers
{
    using Forum.App.ViewModels.Post;
    using Forum.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class PostController : Controller
    {
        private readonly IPostServices postServices;

        public PostController(IPostServices service)
        {
            this.postServices = service;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<PostViewModel> allPosts = await postServices.ListAllPosts();

            return View(allPosts);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await postServices.AddPostAsync(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while adding this post!");

                return View(model);
            }

            return RedirectToAction("All", "Post");
        }

        public async Task<IActionResult> Edit(string id)
        {
            PostFormModel postModel =
                    await this.postServices.EditPost(id);

            return View(postModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PostFormModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(postModel);
            }

            try
            {
                await this.postServices.EditByIdAsync(id, postModel);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while updating your post!");

                return View(postModel);
            }

            return RedirectToAction("All", "Post");
        }
    }
}