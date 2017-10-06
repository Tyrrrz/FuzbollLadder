using System.Linq;
using FuzbollLadder.Services;
using FuzbollLadder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace FuzbollLadder.Controllers
{
    [Route("api/players")]
    public class PlayersApiController : Controller
    {
        private readonly IDataService _dataService;
        public PlayersApiController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        [Route("all")]
        public IActionResult GetAllPlayers()
        {
            var players = _dataService.GetAllPlayers().ToList();
            return Ok(players);
        }
    }
}