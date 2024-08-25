using ClientAgretTarget.Models;
using ClientAgretTarget.Services;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClientAgretTarget.Controllers
{
    public class MissionManagemenController(IHttpClientFactory clientFactory,IAgentService agentService,IMissionManagemenService missionManagemenService) : Controller
    {
        private readonly string _baseUrl = "https://localhost:7299";
        public async Task<IActionResult> Index()
        {
            try
            {
                List<MissionManagementVM>? agents = await missionManagemenService.GetAll();

                return View(agents);
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", " Home");
            }
        }
    }
}
