namespace SeminarHub.Models
{
    using System.ComponentModel.DataAnnotations;
    using static SeminarHub.Utilities.GlobalConstants.SeminarConstants;

    public class EditSeminarViewModel
    {
        public EditSeminarViewModel()
        {
            Categories = new List<CategotyViewModel>();
        }

        [Required]
        public int Id { get; set; }


        [Required]
        [StringLength(TopicMaxLength, MinimumLength = TopicMinLength)]
        public string Topic { get; set; } = string.Empty;


        [Required]
        [StringLength(LecturerMaxLength, MinimumLength = LecturerMinLength)]
        public string Lecturer { get; set; } = string.Empty;


        [Required]
        [StringLength(DetailsMaxLength, MinimumLength = DetailsMinLength)]
        public string Details { get; set; } = string.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        public string DateAndTime { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue)]
        public int Duration { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategotyViewModel> Categories { get; set; }
    }
}
