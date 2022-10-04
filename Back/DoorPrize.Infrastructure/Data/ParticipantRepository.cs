using DoorPrize.ApplicationCore.Entities;
using DoorPrize.ApplicationCore.Exceptions;
using DoorPrize.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DoorPrize.Infrastructure.Data
{
    public class ParticipantRepository : EFRepository, IParticipantRepository
    {
        public ParticipantRepository(IEFContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<ParticipantEntity>> ListElderly()
        {
            var query = EFContext.Set<ParticipantEntity>()
                .Where(participant => participant.Quota.Trim().ToUpper() == "IDOSO");

            try
            {
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new EfQueryException(ex, query.ToQueryString());
            }
        }

        public async Task<IEnumerable<ParticipantEntity>> ListPhysicallyHandicapped()
        {
            var query = EFContext.Set<ParticipantEntity>()
                .Where(participant => participant.Quota.Trim().ToUpper() == "DEIFICENTE FÍSICO" && !string.IsNullOrEmpty(participant.CID));

            try
            {
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new EfQueryException(ex, query.ToQueryString());
            }
        }

        public async Task<IEnumerable<ParticipantEntity>> ListGeneral()
        {
            var query = EFContext.Set<ParticipantEntity>()
                .Where(participant => !(participant.Quota.Trim().ToUpper() == "DEIFICENTE FÍSICO" && !string.IsNullOrEmpty(participant.CID)) && participant.Quota.Trim().ToUpper() != "IDOSO");

            try
            {
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new EfQueryException(ex, query.ToQueryString());
            }
        }

        public async Task Insert(ParticipantEntity entity)
        {
            try
            {
                await EFContext.Set<ParticipantEntity>().AddAsync(entity);
                await EFContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EfEntityException(ex, entity);
            }
        }
    }
}