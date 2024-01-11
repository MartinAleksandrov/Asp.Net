using Library.Data.Models;

namespace Library.Models
{
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Categories = new List<Category>();
        }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Url { get; set; } = null!;

        public decimal Rating { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
