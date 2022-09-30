using DoorPrize.ApplicationCore.Interfaces;

namespace DoorPrize.Infrastructure.Data
{
    public class EFRepository
    {
        public IEFContext EFContext { get; private set; }

        public EFRepository(IEFContext dbContext)
        {
            EFContext = dbContext;
        }
    }
}