using ClientAgretTarget.Services;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClientAgretTarget.Controllers
{
    public class MissionsController(IMissionService missionService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                List<MissionVM>? Missions = await missionService.GetAll();

                return View(Missions);
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
                MissionVM? mission = await missionService.Details(id);

                return View(mission);
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", "home");
            }
        }
        public async Task<IActionResult> RunAMission(int id)
        {
            try
            {
                await missionService.RunAMission(id);

                return RedirectToAction("index", "home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
