using System.Linq;
using FuzbollLadder.Services;
using FuzbollLadder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace FuzbollLadder.Controllers
{
    [Route("api/playerstats")]
    public class PlayerStatsApiController : Controller
    {
        private readonly IDataService _dataService;
        public PlayerStatsApiController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Index()
        {
            var playerStats = _dataService.GetAllPlayerStats().ToArray();
            return Ok(playerStats);
        }
    }
}