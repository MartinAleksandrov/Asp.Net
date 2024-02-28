namespace House_Renting.Web.ViewModels.House
{
    using static HouseRenting.Common.EntityValidationsConstants.House;
    using System.ComponentModel.DataAnnotations;
    public class HouseAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;


        public string Address { get; set; } = null!;


        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Mountly Price")]

        public decimal PricePerMonth { get; set; }

        [Display(Name ="Rented")]
        public bool IsRented { get; set; }
    }
}
