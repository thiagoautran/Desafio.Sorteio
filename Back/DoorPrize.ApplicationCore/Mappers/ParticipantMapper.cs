using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;
using DoorPrize.ApplicationCore.Entities;

namespace DoorPrize.ApplicationCore.Mappers
{
    public static class ParticipantMapper
    {
        public static GetResponse ToGetResponse(this IEnumerable<ParticipantEntity> elderly, IEnumerable<ParticipantEntity> physicallyHandicapped, IEnumerable<ParticipantEntity> general)
        {
            return new GetResponse
            {
                Elderly = elderly.ToParticipantResponse(),
                PhysicallyHandicapped = physicallyHandicapped.ToParticipantResponse(),
                General = general.ToParticipantResponse(),
                NumberParticipants = elderly.Count() + physicallyHandicapped.Count() + general.Count()
            };
        }

        private static IEnumerable<DTOs.Response.Participant.Get.ParticipantResponse> ToParticipantResponse(this IEnumerable<ParticipantEntity> participants)
        {
            var list = new List<DTOs.Response.Participant.Get.ParticipantResponse>();

            foreach (var item in participants)
            {
                list.Add(new DTOs.Response.Participant.Get.ParticipantResponse
                {
                    Name = item.Name,
                    CPF = item.CPF.ToString(@"000\.000\.000\-00")
                });
            }

            return list;
        }

        public static WinnerResponse ToWinnerResponse (this ParticipantEntity elderly, ParticipantEntity physicallyHandicapped, IEnumerable<ParticipantEntity> general)
        {
            var list = new List<DTOs.Response.Participant.Winners.ParticipantResponse>();

            foreach(var item in general)
            {
                list.Add(new DTOs.Response.Participant.Winners.ParticipantResponse
                {
                    Name = item.Name,
                    CPF = item.CPF.ToString(@"000\.000\.000\-00")
                });
            }

            return new WinnerResponse
            {
                Elderly = new DTOs.Response.Participant.Winners.ParticipantResponse
                {
                    Name = elderly.Name,
                    CPF = elderly.CPF.ToString(@"000\.000\.000\-00")
                },
                PhysicallyHandicapped = new DTOs.Response.Participant.Winners.ParticipantResponse
                {
                    Name = physicallyHandicapped.Name,
                    CPF = physicallyHandicapped.CPF.ToString(@"000\.000\.000\-00")
                },
                General = list
            };
        }
    }
}