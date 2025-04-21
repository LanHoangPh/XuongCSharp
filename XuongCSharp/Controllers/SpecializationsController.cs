using Microsoft.AspNetCore.Mvc;

namespace XuongCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationsController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SpecializationsController> _logger;

        public SpecializationsController(
            AppDbContext context,
            ILogger<SpecializationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<MajorDto>>> GetSpecializations()
        {
            try
            {
                var specializations = await _context.Majors
                    .Where(m => m.Status == 1)
                    .Select(m => new MajorDto
                    {
                        Id = m.Id,
                        Code = m.Code!,
                        Name = m.Name!
                    })
                    .ToListAsync();

                return specializations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving specializations");
                return StatusCode(500, "An error occurred while retrieving specializations");
            }
        }

        [HttpGet("by-department/{locationId}/{departmentId}")]
        public async Task<ActionResult<List<MajorDto>>> GetSpecializationsByDepartment(Guid locationId, Guid departmentId)
        {
            try
            {
                var departmentFacility = await _context.DepartmentFacilities
                    .FirstOrDefaultAsync(df =>
                        df.IdFacility == locationId &&
                        df.IdDepartment == departmentId &&
                        df.Status == 1);

                if (departmentFacility == null)
                    return new List<MajorDto>();

                var specializations = await _context.MajorFacilities
                    .Where(mf => mf.IdDepartmentFacility == departmentFacility.Id && mf.Status == 1)
                    .Include(mf => mf.Major)
                    .Where(mf => mf.Major!.Status == 1)
                    .Select(mf => new MajorDto
                    {
                        Id = mf.Major!.Id,
                        Code = mf.Major.Code!,
                        Name = mf.Major.Name!
                    })
                    .ToListAsync();

                return specializations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving specializations by department ID {DepartmentId} at location ID {LocationId}", departmentId, locationId);
                return StatusCode(500, "An error occurred while retrieving specializations");
            }
        }
    }
}
