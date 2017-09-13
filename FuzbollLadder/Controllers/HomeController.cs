using System;
using System.Collections.Generic;
using System.Linq;
using FuzbollLadder.Contracts;
using FuzbollLadder.Data;
using FuzbollLadder.Models;
using FuzbollLadder.Services;
using Microsoft.AspNetCore.Mvc;
using Tyrrrz.Extensions;

namespace FuzbollLadder.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        private readonly IRatingService _ratingService;

        public HomeController(DataContext context, IRatingService ratingService)
        {
            _context = context;
            _ratingService = ratingService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var players = _context.Players.ToArray();

            return View(players);
        }

        [HttpGet("/RegisterPlayer")]
        public IActionResult RegisterPlayer()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/RegisterPlayer")]
        public IActionResult RegisterPlayer(RegisterPlayerViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (_context.Players.Any(p => p.Name.Equals(vm.Name, StringComparison.OrdinalIgnoreCase)))
                return BadRequest($"Player with name [{vm.Name}] already exists");

            var player = new Player
            {
                Name = vm.Name
            };

            _context.Players.Add(player);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("/SubmitMatch")]
        public IActionResult SubmitMatch()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost("/SubmitMatch")]
        public IActionResult SubmitMatch(SubmitMatchViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get player names
            var winnerNames = vm.WinnerNamesCsv.Split(",", ";").Select(n => n.Trim()).ToArray();
            var loserNames = vm.LoserNamesCsv.Split(",", ";").Select(n => n.Trim()).ToArray();

            // Validate count
            if (!winnerNames.Any() || !loserNames.Any())
                return BadRequest("At least one winner or loser is required");

            // Get players
            var winners = new List<Player>();
            var losers = new List<Player>();

            foreach (var playerName in winnerNames)
            {
                var player =
                    _context.Players.FirstOrDefault(
                        p => p.Name.StartsWith(playerName, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                    return BadRequest($"Could not find player [{playerName}]");
                winners.Add(player);
            }
            foreach (var playerName in loserNames)
            {
                var player =
                    _context.Players.FirstOrDefault(
                        p => p.Name.StartsWith(playerName, StringComparison.OrdinalIgnoreCase));
                if (player == null)
                    return BadRequest($"Could not find player [{playerName}]");
                losers.Add(player);
            }

            // Calculate average ratings
            var avgWinnerRating = winners.Average(p => p.Rating);
            var avgLoserRating = losers.Average(p => p.Rating);
            var ratingDelta = _ratingService.CalculateDelta(avgWinnerRating, avgLoserRating);

            // Update stats
            foreach (var player in winners)
            {
                player.Wins++;
                player.Rating += ratingDelta.WinnerDelta;
            }
            foreach (var player in losers)
            {
                player.Losses++;
                player.Rating += ratingDelta.LoserDelta;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}