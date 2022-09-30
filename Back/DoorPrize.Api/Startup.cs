using DoorPrize.Api.Configurations;
using DoorPrize.Api.Configurations.Filters;
using DoorPrize.ApplicationCore.Interfaces;

namespace DoorPrize.Api
{
    public class Startup<I>
        where I : class, IDependencyInjectionConfiguration, new()
    {
        public IConfiguration Configuration { get; }
        public I DependencyInjection { get; }

        public Startup(IConfiguration configuration) =>
            (Configuration, DependencyInjection) = (configuration, new I());

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGenSetup();

            DependencyInjection.AddLogger(services);
            DependencyInjection.AddAppSettings(services, Configuration);
            DependencyInjection.AddDatabase(services);
            DependencyInjection.AddApplicationCore(services);
            DependencyInjection.AddInfrastructure(services);

            services.AddControllers();
            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddApiVersioning();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseSwaggerUISetup();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}