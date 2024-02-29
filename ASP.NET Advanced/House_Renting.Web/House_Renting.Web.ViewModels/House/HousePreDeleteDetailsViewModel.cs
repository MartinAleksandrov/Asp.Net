namespace House_Renting.Web.ViewModels.House
{
    using System.ComponentModel.DataAnnotations;
    public class HousePreDeleteDetailsViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = string.Empty;

    }
}
