namespace MyWebSite.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
