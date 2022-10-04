using DoorPrize.ApplicationCore.Interfaces;
using DoorPrize.ApplicationCore.Mappers;
using DoorPrize.ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoorPrize.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParticipantIntegration _participantIntegration; 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IParticipantIntegration participantIntegration) =>
            (_logger, _participantIntegration) = (logger, participantIntegration);

        public async Task<IActionResult> Index()
        {
            var participant = await _participantIntegration.Get();
            return View(participant.ToParticipantsViewModel());
        }

        public async Task<IActionResult> Winners()
        {
            var participant = await _participantIntegration.GetWinner();
            return View(participant.ToWinnersViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}