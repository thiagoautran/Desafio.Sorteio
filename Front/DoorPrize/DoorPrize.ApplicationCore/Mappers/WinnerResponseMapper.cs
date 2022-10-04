using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;
using DoorPrize.ApplicationCore.Models;

namespace DoorPrize.ApplicationCore.Mappers
{
    public static class WinnerResponseMapper
    {
        public static WinnersViewModel ToWinnersViewModel(this WinnerResponse winnerResponse)
        {
            return new WinnersViewModel
            {
                Elderly = new ParticipantViewModel
                {
                    Name = winnerResponse.Elderly.Name,
                    CPF = winnerResponse.Elderly.CPF
                },
                PhysicallyHandicapped = new ParticipantViewModel
                {
                    Name = winnerResponse.PhysicallyHandicapped.Name,
                    CPF = winnerResponse.PhysicallyHandicapped.CPF
                },
                General = winnerResponse.General.ToParticipantsViewModel()
            };
        }

        public static IEnumerable<ParticipantViewModel> ToParticipantsViewModel(this IEnumerable<ParticipantResponse> participants)
        {
            var list = new List<ParticipantViewModel>();

            foreach (var participant in participants)
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