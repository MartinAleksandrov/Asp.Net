namespace Homies.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    using Homies.Utilities;
    public class Type
    {
        public Type()
        {
            Events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.TypeNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public ICollection<Event> Events { get; set; }
    }
}