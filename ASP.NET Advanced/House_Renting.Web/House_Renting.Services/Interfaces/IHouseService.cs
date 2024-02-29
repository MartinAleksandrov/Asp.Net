namespace House_Renting.Services.Interfaces
{
    using House_Renting.Services.DataModels.House;
    using House_Renting.Web.ViewModels.Home;
    using House_Renting.Web.ViewModels.House;

    public interface IHouseService
    {
        Task<IEnumerable<IndexViewModel>> LastFreeHousesAsync();

        Task<string> CreateAsync(HouseFormModel formModel, string agentId);

        Task<AllHousesfilteredAndPagedServiceModel> AllAync(AllHousesQueryModel queryModel);

        Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);
        Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId);

        Task<HouseDetailsViewModel?> GetDetailsByIdAsync(string houseId);

        Task<bool> ExistsByIdAsync(string houseId);

        Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId);

        Task EditHouseByIdAndFormModelAsync(string houseId, HouseFormModel formModel);

        Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId,string agentId);

        Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId);

        Task DeleteHouseByIdAsync(string houseId);
    }
}