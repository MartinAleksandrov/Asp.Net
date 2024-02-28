namespace House_Renting.Services.DataModels.House
{
    using House_Renting.Web.ViewModels.House;
    public class AllHousesfilteredAndPagedServiceModel
    {
        public AllHousesfilteredAndPagedServiceModel()
        {
            Houses = new List<HouseAllViewModel>();
        }

        public int TotalHousesCount { get; set; }

        public IEnumerable<HouseAllViewModel> Houses { get; set; }
    }
}
