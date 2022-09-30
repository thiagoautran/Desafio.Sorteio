using DoorPrize.Api;
using DoorPrize.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup<DependencyInjectionConfiguration>(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();