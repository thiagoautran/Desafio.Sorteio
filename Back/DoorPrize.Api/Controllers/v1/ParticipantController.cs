using DoorPrize.ApplicationCore.DTOs.Request.Account;
using DoorPrize.ApplicationCore.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace DoorPrize.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("door.prize/v{version:apiVersion}/[controller]")]
    public class ParticipantController : ControllerBase
    {
        [HttpGet("elderly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> Elderly([FromBody] CreditRequest request)
        {
            return Ok();
        }

        [HttpGet("physically_handicapped")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> PhysicallyHandicapped([FromBody] CreditRequest request)
        {
            return Ok();
        }

        [HttpGet("General")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> General([FromBody] CreditRequest request)
        {
            return Ok();
        }
    }
}