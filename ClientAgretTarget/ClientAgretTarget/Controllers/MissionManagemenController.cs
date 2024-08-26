using ClientAgretTarget.Models;
using ClientAgretTarget.Services;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ClientAgretTarget.Controllers
{
    public class MissionManagemenController(IHttpClientFactory clientFactory,IMissionManagemenService missionManagemenService,IMissionService missionService) : Controller
    {
        private readonly string _baseUrl = "https://localhost:7299";
        public async Task<IActionResult> Index()
        {
            try
            {
                List<MissionManagementVM>? missions = await missionManagemenService.GetAll();

                return View(missions);
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
                MissionManagementVM? mission = await missionManagemenService.Details(id);

                return View(mission);
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", "Home");
            }

        }
        public async Task<IActionResult> RunAMission(int id)
        {
            try
            {
                await missionService.RunAMission(id);

                return RedirectToAction("index", "Home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
