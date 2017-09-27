using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Index()
        {
            var matches = _dataService.GetAllMatches().ToArray();
            return View(matches);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddViewModel vm)
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
            var players = _dataService.GetAllPlayers();
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Delete match
            _dataService.DeleteMatch(id);

            // Recalculate all
            _dataService.RecalculateMatches();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Recalculate()
        {
            _dataService.RecalculateMatches();

            return RedirectToAction("Index");
        }
    }
}