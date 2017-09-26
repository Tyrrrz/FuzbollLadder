using System.Linq;
using FuzbollLadder.Services;
using FuzbollLadder.ViewModels.Players;
using Microsoft.AspNetCore.Mvc;

namespace FuzbollLadder.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IDataService _dataService;

        public PlayersController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            var players = _dataService.GetAllPlayers().ToArray();
            return View(players);
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

            _dataService.AddPlayer(vm.Name);

            return RedirectToAction("Index");
        }
    }
}