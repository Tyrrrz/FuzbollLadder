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

            var players = _dataService.GetAllPlayers().ToArray();
            var winners = players.Where(p => vm.WinnerIds.Contains(p.Id));
            var losers = players.Where(p => vm.LoserIds.Contains(p.Id));
            
            // Add match
            var match = _dataService.AddMatch(DateTime.Now, winners, losers);

            return this.Json(match);
        }

        [HttpDelete]
        [Route("{id}")]
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