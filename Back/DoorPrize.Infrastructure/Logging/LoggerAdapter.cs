using DoorPrize.ApplicationCore.Interfaces;
using Serilog;

namespace DoorPrize.Infrastructure.Logging
{
    public class LoggerAdapter<L> : IAppLogger<L>
    {
        private readonly ILogger _logger;

        public LoggerAdapter(ILogger logger) => _logger = logger;

        public void LogInformation(string message)
        {
            _logger.Information(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }
    }
}