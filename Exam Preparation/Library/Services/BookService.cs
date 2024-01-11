namespace Library.Services
{
    using Library.Contracts;
    using Library.Data;
    using Library.Data.Models;
    using Library.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<AddBookViewModel> AddBookAsync(AddBookViewModel model)
        {

            throw new NotImplementedException();
        }


        public async Task AddToCollectionAsync(string userId,BookViewModel model)
        {
            var alreadyAdded = await dbContext.IdentityUserBooks
                .AnyAsync(iu => iu.CollectorId == userId && iu.BookId == model.Id);

            if (!alreadyAdded)
            {
                var userBook = new IdentityUserBook
                {
                    CollectorId = userId,
                    BookId = model.Id
                };

                await dbContext.IdentityUserBooks.AddAsync(userBook);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
            return await dbContext
                .Books
                .Select(b => new AllBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                }).ToListAsync();
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {

            return await dbContext.Books
                                .Where(b => b.Id == id)
                                .Select(b => new BookViewModel
                                {
                                    Id = b.Id,
                                    Title = b.Title,
                                    Author = b.Author,
                                    ImageUrl = b.ImageUrl,
                                    Rating = b.Rating,
                                    Description = b.Description,
                                    CategoryId = b.CategoryId
                                }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string id)
        {
            return await dbContext.IdentityUserBooks.Where(iu => iu.CollectorId == id)
                .Select(iu => new AllBookViewModel
                {
                    Id = iu.BookId,
                    Title = iu.Book.Title,
                    Author = iu.Book.Author,
                    Description = iu.Book.Description,
                    Category = iu.Book.Category.Name,
                    ImageUrl = iu.Book.ImageUrl
                })
                .ToListAsync();
        }

    }
}
