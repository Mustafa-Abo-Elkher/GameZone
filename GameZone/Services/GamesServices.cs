
namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly String _imagePath;
        public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .ToList();
        }
        public Game? GetById(int id)
        {
            return _context.Games
                 .Include(g => g.Category)
                 .Include(g => g.Devices)
                 .ThenInclude(d => d.Device)
                 .AsNoTracking()
                 .SingleOrDefault(g=>g.Id == id);
        }

        public async Task Create(CreateGameFromViewModel model)
        {
            var coverNmae = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";

            var path = Path.Combine(_imagePath,coverNmae);

            using var stream =File.Create(path);
            await model.Cover.CopyToAsync(stream);

            Game game = new ()
            {
             Name = model.Name,
             Description = model.Description,
             CategoryId = model.CategoryId,
             Cover = coverNmae,
             Devices = model.SelectedDevices.Select(d =>new GameDevice { DeviceId =d}).ToList(),
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        
    }
}
