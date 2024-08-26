using ClientAgretTarget.Services;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClientAgretTarget.Controllers
{
    public class TargetsController(ITargetService targetService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TargetVM>? agents = await targetService.GetAll();

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
                TargetVM? agent = await targetService.Details(id);

                return View(agent);
            }
            catch (Exception ex)
            {
                return RedirectToAction("index", "Home");
            }
        }
    }
}
