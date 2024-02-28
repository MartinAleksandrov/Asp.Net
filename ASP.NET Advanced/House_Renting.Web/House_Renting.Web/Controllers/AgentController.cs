namespace House_Renting.Web.Controllers
{
    using House_Renting.Services.Interfaces;
    using House_Renting.Web.ViewModels.Agent;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Web.Infrastructure.Extensions.ClaimsExtenstions;

    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService agent)
        {
            agentService = agent;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            var userId = this.User.GetId();

            bool isAgent = await this.agentService.AgentExistByUserIdAsync(userId!);

            if (isAgent)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentViewModel model)
        {
            var userId = this.User.GetId();

            bool isAgent = await this.agentService.AgentExistByUserIdAsync(userId!);

            if (isAgent)
            {
                return RedirectToAction("Index", "Home");
            }

            var isPhoneNumberTaken = await agentService.AgentExistByPhoneNumberAsync(model.PhoneNumber);

            if (isPhoneNumberTaken)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided phone number already exist");
            }

            if (ModelState.IsValid)
            {
                return View(model);
            }

            var userHaveActiveRents = await agentService.UserHasRentsAsync(userId!);

            if (userHaveActiveRents)
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), "You must not have any active rents in order to become agent!");

                return RedirectToAction("Mine","House");

            }

            try
            {
                await agentService.Create(userId!, model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
                throw;
            }

            return RedirectToAction("All", "House");
        }
    }
}
