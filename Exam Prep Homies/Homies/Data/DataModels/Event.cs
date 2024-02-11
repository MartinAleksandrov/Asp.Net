namespace Homies.Data.DataModels
{
    using Homies.Utilities;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;    

    public class Event
    {
        public Event()
        {
            EventsParticipants = new HashSet<EventsParticipants>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(GlobalConstants.DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Organiser))]
        public string OrganiserId { get; set; } = null!;

        [Required]
        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        [ForeignKey(nameof(Type))]
        public int TypeId { get; set; }

        [Required]
        public Type Type { get; set; } = null!;

        [Required]
        public ICollection<EventsParticipants> EventsParticipants { get; set; }
    }
}