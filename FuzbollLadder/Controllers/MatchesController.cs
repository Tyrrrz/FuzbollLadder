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
    }
}