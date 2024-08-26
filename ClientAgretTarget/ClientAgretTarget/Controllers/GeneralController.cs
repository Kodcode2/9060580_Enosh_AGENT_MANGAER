using ClientAgretTarget.Services;
using ClientAgretTarget.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClientAgretTarget.Controllers
{
    public class GeneralController(IMissionManagemenService missionManagemenService) : Controller
    {
        public async Task<IActionResult> Details()
        {
            GeneralVM? general = await missionManagemenService.GetGeneral();
            return View(general);
        }
    }
}
