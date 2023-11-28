
namespace GameZone.Services
{
    public class DevicesServvices : IDevicesServvices
    {
        private readonly ApplicationDbContext _context;
        public DevicesServvices (ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Devices.Select
            (d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
            .OrderBy(d => d.Text)
            .AsNoTracking()
            .ToList();
        }
    }
}
