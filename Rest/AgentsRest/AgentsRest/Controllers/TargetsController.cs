using AgentsRest.Dto;
using AgentsRest.Models;
using AgentsRest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentsRest.Controllers
{
    [Route("")]
    [ApiController]
    public class TargetsController(ITargetService targetService) : ControllerBase
    {
        [HttpPost("targets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateTarget([FromBody] TargetDto targetDto)
        {
            try
            {
                var targetModel = await targetService.CreateNewTargetAsync(targetDto);
                return Created("new user", new IdDto { Id = targetModel.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("targets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllTarget() =>
            Ok(await targetService.GetAllTargetAsync());

        [HttpPut("targets/{id}/pin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateLocationTarget(LocationDto locationDto, int id)
        {
            try
            {
                await targetService.UpdateLocationTargetAsync(locationDto, id);
                return Created("new user", locationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("targets/{id}/move")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> moveTarget(DirectionDto directionDto, int id)
        {
            try
            {
                await targetService.moveTargetAsync(directionDto, id);
                return Created("new user", directionDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
