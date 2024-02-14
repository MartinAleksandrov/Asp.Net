using Library.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Services
{
    public class BookServices : Controller
    {

        private readonly LibraryDbContext context;


        public BookServices(LibraryDbContext DBcontext)
        {
            context = DBcontext;
        }

        public string Get()
        {

        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
