namespace Homies.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Homies.Utilities.GlobalConstants;

    public class CreateEventViewModel
    {

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(EventNameMaxLength,
            MinimumLength = EventNameMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(DescriptionMaxLength,
             MinimumLength = DescriptionMinLength,
             ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = RequireErrorMessage)]
        public string Start { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        public string End { get; set; } = String.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        public int TypeId { get; set; }

        [Required]
        public List<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();

    }
}
