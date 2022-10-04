using DoorPrize.ApplicationCore.DTOs.Response;
using DoorPrize.ApplicationCore.DTOs.Response.Participant;
using DoorPrize.ApplicationCore.Interfaces;
using DoorPrize.ApplicationCore.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace DoorPrize.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("door.prize/v{version:apiVersion}/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService) =>
            _participantService = participantService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> FileUpload([FromForm] IFormFile arquivo)
        {
            await _participantService.FileUpload(arquivo);
            return Ok();
        }

        [HttpGet("elderly")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParticipantResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> Elderly() =>
            Ok((await _participantService.ListElderly()).ToParticipantResponse());

        [HttpGet("physically_handicapped")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParticipantResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> PhysicallyHandicapped() =>
            Ok((await _participantService.ListPhysicallyHandicapped()).ToParticipantResponse());

        [HttpGet("general")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ParticipantResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> General() =>
            Ok((await _participantService.ListGeneral()).ToParticipantResponse());

        [HttpGet("winners")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WinnerResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(InternalServerErrorResponse))]
        public async Task<IActionResult> Winners()
        {
            var elderly = await _participantService.WinnerElderly();
            var physicallyHandicapped = await _participantService.WinnerPhysicallyHandicapped();
            var general = await _participantService.WinnerGeneral();

            return Ok(elderly.ToWinnerResponse(physicallyHandicapped, general));
        }
    }
}