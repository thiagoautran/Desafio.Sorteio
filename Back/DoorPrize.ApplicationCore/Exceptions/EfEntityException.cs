using System.Text.Json;

namespace DoorPrize.ApplicationCore.Exceptions
{
    public class EfEntityException : Exception
    {
        public string Entity { get; set; }

        public EfEntityException(Exception exception, object entity) : base(exception.Message, exception) =>
            Entity = JsonSerializer.Serialize(entity);
    }
}