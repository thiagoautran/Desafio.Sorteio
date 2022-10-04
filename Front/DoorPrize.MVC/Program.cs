using DoorPrize.ApplicationCore.Interfaces;
using DoorPrize.Infrastructure.DoorPrize;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IParticipantIntegration, ParticipantIntegration>();
builder.Services.AddHttpClient("PARTICIPANT", c =>
{
    c.BaseAddress = new Uri("http://host.docker.internal:8003");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
