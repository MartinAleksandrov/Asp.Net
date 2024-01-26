namespace House_Renting.Services.Interfaces
{
    using House_Renting.Web.ViewModels.Home;
    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastFreeHousesAsync();
    }
}
