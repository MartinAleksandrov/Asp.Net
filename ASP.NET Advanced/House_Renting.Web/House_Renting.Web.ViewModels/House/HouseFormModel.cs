namespace House_Renting.Web.ViewModels.House
{
    using System.ComponentModel.DataAnnotations;
    using House_Renting.Web.ViewModels.Category;
    using static HouseRenting.Common.EntityValidationsConstants.House;
    public class HouseFormModel
    {

        public HouseFormModel()
        {
            Categories = new List<CategoryViewModel>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength =TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(DesctiptionMaxLength, MinimumLength = DesctiptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImgMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        [Required]
        [Range(typeof(decimal), PricePerMounthMinValue,PricePerMounthMaxValue)]
        public decimal PricePerMonth { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
