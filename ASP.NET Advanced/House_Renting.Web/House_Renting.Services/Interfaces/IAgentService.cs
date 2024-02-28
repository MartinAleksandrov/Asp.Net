namespace House_Renting.Services.Interfaces
{
    using House_Renting.Web.ViewModels.Agent;
    public interface IAgentService
    {
        Task<bool> AgentExistByUserIdAsync(string userId);
        Task<bool> AgentExistByPhoneNumberAsync(string number);
        Task<bool> UserHasRentsAsync(string userId);
        Task Create(string userId, BecomeAgentViewModel model);

        Task<string?> GetAgentIdByUserIdAsync(string userId);

    }
}
