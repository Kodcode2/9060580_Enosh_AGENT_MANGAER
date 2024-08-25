using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("")]
    [ApiController]
    public class AgentsController(IAgentService agentService) : ControllerBase
    {
        [HttpPost("agents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] AgentDto agentDto)
        {
            try
            {
                var agentModel = await agentService.CreateNewAgentAsync(agentDto);
                return Created("new user", new IdDto { Id = agentModel.Id});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("agents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  async Task<ActionResult> GetAllAgent() =>
            Ok(await agentService.GetAllAgentAsync());

        [HttpPut("agents/{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateLocationAgent( LocationDto locationDto,int id)
        {
            try
            {
                await agentService.UpdateLocationAgentAsync(locationDto , id);
                return Created("new user", locationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("agents/{id}/move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> moveAgent(DirectionDto directionDto, int id)
        {
            try
            {
                await agentService.moveAgentAsync(directionDto, id);
                return Created("new user", directionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
