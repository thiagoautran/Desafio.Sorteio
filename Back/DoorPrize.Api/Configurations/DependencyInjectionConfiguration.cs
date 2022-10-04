using DoorPrize.ApplicationCore.DTOs.AppSettings;
using DoorPrize.ApplicationCore.Interfaces;
using DoorPrize.ApplicationCore.Services;
using DoorPrize.Infrastructure.Data;
using DoorPrize.Infrastructure.File;
using DoorPrize.Infrastructure.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace DoorPrize.Api.Configurations
{
    public class DependencyInjectionConfiguration : IDependencyInjectionConfiguration
    {
        public virtual void AddLogger(IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            services.AddSingleton<Serilog.ILogger>(x => logger);
        }

        public virtual AppSettings AddAppSettings(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            var appSettings = new AppSettings();
            new ConfigureFromConfigurationOptions<AppSettings>(configuration.GetSection("AppSettings")).Configure(appSettings);

            return appSettings;
        }

        public virtual void AddApplicationCore(IServiceCollection services)
        {
            services.AddScoped<IParticipantService, ParticipantService>();
        }

        public virtual void AddDatabase(IServiceCollection services) =>
            services.AddScoped<IEFContext, EFContext>();

        public virtual void AddInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IParticipantFile, ParticipantFile>();
        }
    }
}