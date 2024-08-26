using AgentsRest.Dto;
using AgentsRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace AgentsRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        private static readonly ImmutableList<string> allowedNames = [
    "SimulationServer", "MVCServer"
];

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                TokenDto token = new() { Token = loginService.Login(loginDto.id) };
                return Ok(token);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
