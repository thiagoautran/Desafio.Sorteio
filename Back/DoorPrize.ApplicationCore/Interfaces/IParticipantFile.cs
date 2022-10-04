using DoorPrize.ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;

namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IParticipantFile
    {
        Task<IList<ParticipantEntity>> Get(IFormFile file);
    }
}