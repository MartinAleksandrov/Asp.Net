namespace SeminarHub.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SeminarParticipant
    {
        [Required]
        [ForeignKey(nameof(Seminar))]
        public int SeminarId { get; set; }

        [Required]
        public Seminar Seminar { get; set; } = null!;


        [Required]
        [ForeignKey(nameof(Participant))]
        public string ParticipantId { get; set; } = string.Empty;

        [Required]
        public IdentityUser Participant { get; set; } = null!;
    }
}
