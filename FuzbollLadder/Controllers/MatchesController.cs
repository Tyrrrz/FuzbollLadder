using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzbollLadder.Models;
using FuzbollLadder.Services;
using FuzbollLadder.ViewModels.Matches;
using Microsoft.AspNetCore.Mvc;

namespace FuzbollLadder.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IDataService _dataService;

        public MatchesController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IActionResult> Index()
        {
            var matches = await _dataService.GetAllMatchesAsync();
            return View(matches);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMatchViewModel vm)
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
            var players = await _dataService.GetAllPlayersAsync();
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
            await _dataService.AddMatchAsync(DateTime.UtcNow, winners, losers);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Recalculate()
        {
            await _dataService.RecalculateMatchesAsync();

            return RedirectToAction("Index");
        }
    }
}