using DoorPrize.Api;
using DoorPrize.Api.Configurations;
    
Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup<DependencyInjectionConfiguration>>();
    }).Build().Run();