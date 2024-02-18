namespace SeminarHub.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static SeminarHub.Utilities.GlobalConstants.SeminarConstants;

    public class Seminar
    {
        public Seminar()
        {
            SeminarsParticipants = new List<SeminarParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TopicMaxLength)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [MaxLength(LecturerMaxLength)]
        public string Lecturer { get; set; } = string.Empty;


        [Required]
        [MaxLength(DetailsMaxLength)]
        public string Details { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(Organizer))]
        public string OrganizerId { get; set; } = string.Empty;

        [Required]
        public IdentityUser Organizer { get; set; } = null!;


        [Required]
        public DateTime DateAndTime { get; set; }


        [Required]
        [MaxLength(DurationMaxLength)]
        public int Duration { get; set; }


        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public ICollection<SeminarParticipant> SeminarsParticipants { get; set; }
    }
}