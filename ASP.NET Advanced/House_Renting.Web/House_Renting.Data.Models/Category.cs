namespace House_Renting.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRenting.Common.EntityValidationsConstants.Category;
    public class Category
    {
        public Category()
        {
            Houses = new List<House>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<House> Houses { get; set; } 
    }
}