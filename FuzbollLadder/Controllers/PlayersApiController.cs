﻿using System.Linq;
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
        [Route("[action]")]
        public IActionResult All()
        {
            var players = _dataService.GetAllPlayers().ToList();
            return Ok(players);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Add([FromBody] AddViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var player = _dataService.AddPlayer(vm.Name);
            return this.Json(player);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Statistics()
        {
            var playerStats = _dataService.GetAllPlayerStats().ToArray();
            return Ok(playerStats);
        }
    }
}