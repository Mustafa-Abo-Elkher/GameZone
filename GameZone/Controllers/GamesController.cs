
namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
      
        private readonly ICategoriesService _categoriesService;
        private readonly IDevicesServvices _devicesServvices;
        private readonly IGamesServices _gamesService;

        public GamesController( ICategoriesService categoriesService,
            IDevicesServvices devicesServvices, IGamesServices gamesServices)
        {

            _categoriesService = categoriesService;
            _devicesServvices = devicesServvices;
            _gamesService = gamesServices;
        }

        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }
        public IActionResult Details (int id)
        {
          var game =_gamesService.GetById(id);
            if (game == null)
                return NotFound();

            return View(game);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFromViewModel viewmodel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _devicesServvices.GetSelectList(),

            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFromViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _devicesServvices.GetSelectList();
                return View(model);
            }
           await _gamesService.Create(model);

            return RedirectToAction(nameof(Index));
        }
    }
}