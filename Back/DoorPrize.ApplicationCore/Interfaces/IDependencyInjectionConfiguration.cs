using DoorPrize.ApplicationCore.DTOs.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoorPrize.ApplicationCore.Interfaces
{
    public interface IDependencyInjectionConfiguration
    {
        void AddLogger(IServiceCollection services);
        AppSettings AddAppSettings(IServiceCollection services, IConfiguration configuration);
        void AddApplicationCore(IServiceCollection services);
        void AddInfrastructure(IServiceCollection services);
        void AddDatabase(IServiceCollection services);
    }
}