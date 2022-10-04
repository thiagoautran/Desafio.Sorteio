using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;

namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IParticipantFacade
    {
        Task<GetResponse> Get();
        Task<WinnerResponse> Winners();
    }
}