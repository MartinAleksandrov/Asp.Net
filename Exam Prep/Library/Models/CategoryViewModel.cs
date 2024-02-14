namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Library.Utilities.Constants.CategoryConstants;
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxCategoryName, MinimumLength = MinCategoryName)]
        public string Name { get; set; } = string.Empty;
    }
}
