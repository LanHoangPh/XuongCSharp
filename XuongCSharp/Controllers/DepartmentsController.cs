using Microsoft.AspNetCore.Mvc;

namespace XuongCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(AppDbContext context, ILogger<DepartmentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDto>>> GetDepartments()
        {
            try
            {
                var departments = await _context.Departments
                    .Where(d => d.Status == 1)
                    .Select(d => new DepartmentDto
                    {
                        Id = d.Id,
                        Code = d.Code!,
                        Name = d.Name!
                    })
                    .ToListAsync();

                return departments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving departments");
                return StatusCode(500, "An error occurred while retrieving departments");
            }
        }

        [HttpGet("by-location/{locationId}")]
        public async Task<ActionResult<List<DepartmentDto>>> GetDepartmentsByLocation(Guid locationId)
        {
            try
            {
                var departments = await _context.DepartmentFacilities
                    .Where(df => df.IdFacility == locationId && df.Status == 1)
                    .Include(df => df.Department)
                    .Where(df => df.Department!.Status == 1)
                    .Select(df => new DepartmentDto
                    {
                        Id = df.Department!.Id,
                        Code = df.Department.Code!,
                        Name = df.Department.Name!
                    })
                    .Distinct()
                    .ToListAsync();

                return departments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving departments by location ID {LocationId}", locationId);
                return StatusCode(500, "An error occurred while retrieving departments");
            }
        }
    }
}
