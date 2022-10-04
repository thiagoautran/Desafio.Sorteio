using DoorPrize.ApplicationCore.DTOs.Response;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;
using DoorPrize.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DoorPrize.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("door.prize/v{version:apiVersion}/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantFacade _participantFacade;
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantFacade participantFacade, IParticipantService participantService) =>
            (_participantFacade, _participantService) = (participantFacade, participantService);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> FileUpload([FromForm] IFormFile arquivo)
        {
            await _participantService.FileUpload(arquivo);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> Get() =>
            Ok(await _participantFacade.Get());

        [HttpGet("winners")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WinnerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> Winners() =>
            Ok(await _participantFacade.Winners());
    }
}