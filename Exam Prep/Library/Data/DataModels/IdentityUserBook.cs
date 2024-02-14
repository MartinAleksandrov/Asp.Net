namespace Library.Data.DataModels
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class IdentityUserBook
    {
        [Required]
        [ForeignKey(nameof(Collector))]
        public string CollectorId { get; set; } = string.Empty;

        [Required]
        public IdentityUser Collector { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }

        [Required]
        public Book Book { get; set; } = null!;
    }
}