namespace House_Renting.Services.Interfaces
{
    public interface IAgentService
    {
        Task<bool> AgentExistByUserId(string userId);
    }
}
