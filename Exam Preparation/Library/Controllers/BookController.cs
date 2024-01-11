namespace Library.Controllers
{
    using Library.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public async Task<IActionResult> All()
        {
            var books = await bookService.GetAllBooksAsync();

            return View(books);
        }

        public async Task<IActionResult> Mine()
        {
            var myBooks = await bookService.GetMyBooksAsync(GetUserId());

            return View(myBooks);
        }

        public async Task<IActionResult> AddToCollection(int id)
        {
            var book = await bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return RedirectToAction(nameof(All));
            }

            var userId = GetUserId();

            await bookService.AddToCollectionAsync(userId, book);

            return RedirectToAction(nameof(All));
        }
    }
}
