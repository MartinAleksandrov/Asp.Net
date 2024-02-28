namespace House_Renting.Services.Interfaces
{
    using House_Renting.Web.ViewModels.Home;
    using House_Renting.Web.ViewModels.House;

    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastFreeHousesAsync();

        Task CreateAsync(HouseFormModel formModel, string agentId);
    }
}
