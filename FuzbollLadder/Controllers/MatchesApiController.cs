using System;
using System.Collections.Generic;
using System.Linq;
using FuzbollLadder.Models;
using FuzbollLadder.Services;
using FuzbollLadder.ViewModels.Matches;
using Microsoft.AspNetCore.Mvc;

namespace FuzbollLadder.Controllers
{
    [Route("api/matches")]
    public class MatchesApiController : Controller
    {
        private readonly IDataService _dataService;
        public MatchesApiController(IDataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult All()
        {
            var matches = _dataService.GetAllMatches().ToArray();
            return Ok(matches);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Add([FromBody] AddViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get player names
            var winnerNames = vm.WinnerNamesCsv.Split(",").Select(n => n.Trim()).ToArray();
            var loserNames = vm.LoserNamesCsv.Split(",").Select(n => n.Trim()).ToArray();

            // Validate names
            if (!winnerNames.Any() || !loserNames.Any())
                return BadRequest("At least one winner and loser is required");

            // Get players
            var players = _dataService.GetAllPlayers().ToArray();
            var winners = new List<Player>();
            var losers = new List<Player>();
            foreach (var playerName in winnerNames)
            {
                var player =
                    players.FirstOrDefault(p => p.Name.StartsWith(playerName, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                    return BadRequest($"Could not find player [{playerName}]");
                winners.Add(player);
            }
            foreach (var playerName in loserNames)
            {
                var player =
                    players.FirstOrDefault(p => p.Name.StartsWith(playerName, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                    return BadRequest($"Could not find player [{playerName}]");
                losers.Add(player);
            }

            // Add match
            _dataService.AddMatch(DateTime.UtcNow, winners, losers);

            return Ok();
        }
    }
}