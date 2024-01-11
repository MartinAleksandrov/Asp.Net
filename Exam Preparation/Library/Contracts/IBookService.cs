namespace Library.Contracts
{
    using Library.Data.Models;
    using Library.Models;

    public interface IBookService
    {
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();

        Task<AddBookViewModel> AddBookAsync(AddBookViewModel model);

        Task<IEnumerable<AllBookViewModel>> GetMyBooksAsync(string id);

        Task AddToCollectionAsync(string id,BookViewModel model);

        Task<BookViewModel?> GetBookByIdAsync(int id);
    }
}
