using ClientAgretTarget.Services;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClientAgretTarget.Controllers
{
    public class AgentsController(IAgentService agentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                List<AgentVM>? agents = await agentService.GetAll();

                return View(agents);
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", "Home");
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                AgentVM? agent = await agentService.Details(id);

                return View(agent);
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", "Home");
            }
        }
    }
}
