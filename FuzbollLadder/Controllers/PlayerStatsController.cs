using System.Linq;
using FuzbollLadder.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuzbollLadder.Controllers
{
    public class PlayerStatsController : Controller
    {
        private readonly IDataService _dataService;

        public PlayerStatsController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            var playerStats = _dataService.GetAllPlayerStats().ToArray();
            return View(playerStats);
        }
    }
}