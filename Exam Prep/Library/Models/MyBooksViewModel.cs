namespace Library.Models
{
    using Microsoft.AspNetCore.Identity;

    public class MyBooksViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;
    }
}