namespace SeminarHub.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SeminarHub.Utilities.GlobalConstants.CategoryConstants;
    public class Category
    {
        public Category()
        {
            Seminars = new List<Seminar>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = string.Empty;

        
        public ICollection<Seminar> Seminars { get; set; }
    }
}