using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.Models;

namespace DoorPrize.ApplicationCore.Mappers
{
    public static class GetResponseMapper
    {
        public static ParticipantsViewModel ToParticipantsViewModel(this GetResponse getResponse)
        {
            return new ParticipantsViewModel
            {
                Elderly = getResponse.Elderly.ToParticipantsViewModel(),
                PhysicallyHandicapped = getResponse.PhysicallyHandicapped.ToParticipantsViewModel(),
                General = getResponse.General.ToParticipantsViewModel(),
                NumberParticipants = getResponse.NumberParticipants
            };
        }

        public static IEnumerable<ParticipantViewModel> ToParticipantsViewModel(this IEnumerable<ParticipantResponse> participants)
        {
            var list = new List<ParticipantViewModel>();

            foreach(var participant in participants)
            {
                list.Add(new ParticipantViewModel
                {
                    Name = participant.Name,
                    CPF = participant.CPF
                });
            }

            return list;
        }
    }
}