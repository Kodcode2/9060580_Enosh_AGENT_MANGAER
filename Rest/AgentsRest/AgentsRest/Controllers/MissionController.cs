using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService) : ControllerBase
    {

        [HttpPut("assignToAMission{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> assignToAMission(int id)
        {
            try
            {
                MissionModel missionModel = await missionService.assignToAMissionAsync(id);
                return Created("new user", missionModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("missions/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void AgentsPursuit()
        {
            await missionService.AgentsPursuitAsync();
        }

        [HttpGet("missions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllMission() => 
            Ok(await missionService.GetAllMissionAsync());
        [HttpPut("missions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async void UpdateStatusMission(int id)
        {
             missionService.CommandmentToMissionAsync(id);
        }

    }
}
