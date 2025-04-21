using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace XuongCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationsController> _logger;

        public LocationsController(
            AppDbContext context,
            IMapper mapper,
            ILogger<LocationsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<FacilityDto>>> GetLocations()
        {
            try
            {
                var locations = await _context.Facilities
                    .Where(f => f.Status == 1)
                    .Select(f => new FacilityDto
                    {
                        Id = f.Id,
                        Code = f.Code,
                        Name = f.Name
                    })
                    .ToListAsync();

                return locations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving locations");
                return StatusCode(500, "An error occurred while retrieving locations");
            }
        }
    }
}
