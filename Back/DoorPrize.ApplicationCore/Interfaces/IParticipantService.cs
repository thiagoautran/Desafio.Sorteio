using DoorPrize.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;

namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IParticipantService
    {
        Task FileUpload(IFormFile arquivo);
        Task<IEnumerable<ParticipantEntity>> ListElderly();
        Task<IEnumerable<ParticipantEntity>> ListPhysicallyHandicapped();
        Task<IEnumerable<ParticipantEntity>> ListGeneral();
        Task<ParticipantEntity> WinnerElderly();
        Task<ParticipantEntity> WinnerPhysicallyHandicapped();
        Task<IEnumerable<ParticipantEntity>> WinnerGeneral();
    }
}