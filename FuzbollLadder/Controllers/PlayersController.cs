using System.Threading.Tasks;
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

        public async Task<IActionResult> Index()
        {
            var players = await _dataService.GetAllPlayersAsync();
            return View(players);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPlayerViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dataService.AddPlayerAsync(vm.Name);

            return RedirectToAction("Index");
        }
    }
}