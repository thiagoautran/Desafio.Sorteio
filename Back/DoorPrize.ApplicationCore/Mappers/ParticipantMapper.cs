using DoorPrize.ApplicationCore.DTOs.Response.Participant;
using DoorPrize.ApplicationCore.Entities;

namespace DoorPrize.ApplicationCore.Mappers
{
    public static class ParticipantMapper
    {
        public static IEnumerable<ParticipantResponse> ToParticipantResponse(this IEnumerable<ParticipantEntity> participants)
        {
            var list = new List<ParticipantResponse>();

            foreach (var item in participants)
            {
                list.Add(new ParticipantResponse
                {
                    Name = item.Name,
                    CPF = item.CPF.ToString(@"000\.000\.000\-00")
                });
            }

            return list;
        }

        public static WinnerResponse ToWinnerResponse (this ParticipantEntity elderly, ParticipantEntity physicallyHandicapped, IEnumerable<ParticipantEntity> general)
        {
            var list = new List<ParticipantResponse>();

            foreach(var item in general)
            {
                list.Add(new ParticipantResponse
                {
                    Name = item.Name,
                    CPF = item.CPF.ToString(@"000\.000\.000\-00")
                });
            }

            return new WinnerResponse
            {
                Elderly = new ParticipantResponse
                {
                    Name = elderly.Name,
                    CPF = elderly.CPF.ToString(@"000\.000\.000\-00")
                },
                PhysicallyHandicapped = new ParticipantResponse
                {
                    Name = physicallyHandicapped.Name,
                    CPF = physicallyHandicapped.CPF.ToString(@"000\.000\.000\-00")
                },
                General = list
            };
        }
    }
}