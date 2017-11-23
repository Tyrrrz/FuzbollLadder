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

            
            // Validate names
            //if (!vm.WinnerName1.Any() || !vm.LoserName1.Any() )
            //    return BadRequest("At least one winner and loser is required");

            // Get players
            var players = _dataService.GetAllPlayers().ToArray();
            var winners = new List<Player>();
            var losers = new List<Player>();
            var winner1 = players.FirstOrDefault(p => p.Name.StartsWith(vm.WinnerName1, StringComparison.OrdinalIgnoreCase));
            winners.Add(winner1);
            var winner2 = players.FirstOrDefault(p => p.Name.StartsWith(vm.WinnerName2, StringComparison.OrdinalIgnoreCase));
            winners.Add(winner2);
            var loser1 = players.FirstOrDefault(p => p.Name.StartsWith(vm.LoserName1, StringComparison.OrdinalIgnoreCase));
            losers.Add(loser1);
            var loser2 = players.FirstOrDefault(p => p.Name.StartsWith(vm.LoserName2, StringComparison.OrdinalIgnoreCase));
            losers.Add(loser2);

            // Add match
            var match = _dataService.AddMatch(DateTime.Now, winners, losers);

            return this.Json(match);
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Delete match
            _dataService.DeleteMatch(id);

            // Recalculate all
            _dataService.RecalculateMatches();

            return Ok();
        }
    }
}