using ClientAgretTarget.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientAgretTarget.Controllers
{
    public class MtrizaController(IMtrizaService mtrizaService) : Controller
    {
        public async Task< IActionResult> Index()
        {
            var res = await mtrizaService.GetAllAgentAndTarget();
            return View(res);
        }
    }
}
