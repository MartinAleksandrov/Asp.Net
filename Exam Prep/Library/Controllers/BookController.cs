namespace Library.Controllers
{
    using Library.Data;
    using Library.Data.DataModels;
    using Library.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;

    [Authorize]
    public class BookController : Controller
    {

        private readonly LibraryDbContext context;

        public BookController(LibraryDbContext dbContext)
        {
            this.context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var allBooks = await context
                .Books
                .AsNoTracking()
                .Select(b => new AllBooksViewModel()
                {
                    Id = b.Id,
                    ImageUrl = b.ImageUrl,
                    Title = b.Title,
                    Author = b.Author,
                    Rating = b.Rating.ToString(),
                    Category = b.Category.Name
                })
                .ToListAsync();

            return View(allBooks);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var books = await context
                .UsersBooks
                .AsNoTracking()
                .Where(u => u.CollectorId == GetUserId())
                .Select(b => new MyBooksViewModel()
                {
                    Id = b.Book.Id,
                    ImageUrl = b.Book.ImageUrl,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    Category = b.Book.Category.Name,
                    Description = b.Book.Description
                })
                .ToListAsync();


            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCollection(int Id)
        {
            var userId = GetUserId();

            var alreadyAdded = await context.UsersBooks.AnyAsync(b => b.CollectorId == userId && b.BookId == Id);

            if (alreadyAdded == false)
            {
                var newBook = new IdentityUserBook()
                {
                    BookId = Id,
                    CollectorId = userId
                };

                await context.UsersBooks.AddAsync(newBook);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCollection(int Id)
        {
            var userBook = await context
                .UsersBooks.FirstOrDefaultAsync(u => u.BookId == Id && u.CollectorId == GetUserId());

            if (userBook != null)
            {
                context.UsersBooks.Remove(userBook);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Mine));
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddBookViewModel();

            model.Categories = GetCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel model)
        {
            var book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                ImageUrl = model.Url,
                CategoryId = model.CategoryId,
                Rating = decimal.Parse(model.Rating)
            };

            if (await context.Books.AnyAsync(b => b.Title == book.Title && b.Author == book.Author))
            {
                return RedirectToAction(nameof(All));
            }

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private List<CategoryViewModel> GetCategories()
        {
            return  context
                .Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}