using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;
using DoorPrize.ApplicationCore.Exceptions;
using DoorPrize.ApplicationCore.Interfaces;
using DoorPrize.ApplicationCore.Mappers;
using System.Collections.Generic;

namespace DoorPrize.ApplicationCore.Facade
{
    public class ParticipantFacade : IParticipantFacade
    {
        private readonly IParticipantService _participantService;

        public ParticipantFacade(IParticipantService participantService) =>
            _participantService = participantService;

        public async Task<GetResponse> Get()
        {
            var elderly = await _participantService.ListElderly();
            var physicallyHandicapped = await _participantService.ListPhysicallyHandicapped();
            var general = await _participantService.ListGeneral();

            if (elderly.Count() == 0 && physicallyHandicapped.Count() == 0 && general.Count() == 0)
                throw new NotFoundException();

            return elderly.ToGetResponse(physicallyHandicapped, general);
        }

        public async Task<WinnerResponse> Winners()
        {
            var elderly = await _participantService.WinnerElderly();
            var physicallyHandicapped = await _participantService.WinnerPhysicallyHandicapped();
            var general = await _participantService.WinnerGeneral();

            if (elderly == null && physicallyHandicapped == null && general.Count() == 0)
                throw new BadRequestException("Não existem participantes cadastrados.");

            return elderly.ToWinnerResponse(physicallyHandicapped, general);
        }
    }
}