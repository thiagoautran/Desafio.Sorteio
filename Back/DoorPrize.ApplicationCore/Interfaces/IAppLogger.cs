namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IAppLogger<L>
    {
        void LogInformation(string message);
        void LogError(string message);
    }
}