using DoorPrize.ApplicationCore.DTOs.Response.Participant.Get;
using DoorPrize.ApplicationCore.DTOs.Response.Participant.Winners;

namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IParticipantIntegration
    {
        Task<GetResponse> Get();
        Task<WinnerResponse> GetWinner();
    }
}