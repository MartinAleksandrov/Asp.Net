namespace House_Renting.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;
    using static HouseRenting.Common.EntityValidationsConstants.Agent;
    public class BecomeAgentViewModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength,MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name ="Phone")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}