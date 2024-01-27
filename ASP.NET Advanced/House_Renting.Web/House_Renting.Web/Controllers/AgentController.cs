namespace House_Renting.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Web.Infrastructure.Extensions.ClaimsExtenstions;

    [Authorize]
    public class AgentController : Controller
    {
        [HttpGet]
        public async  Task<IActionResult> Become()
        {
            var agentId = this.User.GetId();

             return  View();
        }
    }
}
