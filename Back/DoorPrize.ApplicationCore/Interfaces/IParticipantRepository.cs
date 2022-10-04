using DoorPrize.ApplicationCore.Entities;

namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<ParticipantEntity>> ListElderly();
        Task<IEnumerable<ParticipantEntity>> ListPhysicallyHandicapped();
        Task<IEnumerable<ParticipantEntity>> ListGeneral();
        Task Insert(ParticipantEntity entity);
    }
}