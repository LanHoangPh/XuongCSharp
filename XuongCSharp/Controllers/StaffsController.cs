using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XuongCSharp.Import;
namespace XuongCSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly ILogger<StaffsController> _logger;

        public StaffsController(IStaffService staffService, ILogger<StaffsController> logger)
        {
            _staffService = staffService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAllStaff()
        {
            try
            {
                var staffs =  await _staffService.GetAllStaffAsync();
                return Ok(staffs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving staff list");
                return StatusCode(500, "Đã xảy ra lỗi khi lấy dữ liệu nhân viên");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetStaffById(Guid id)
        {
            try
            {
                var staff = await _staffService.GetStaffByIdAsync(id);
                return staff;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving staff with ID {StaffId}", id);
                return StatusCode(500, "Đã xảy ra lỗi khi lấy dữ liệu nhân viên");
            }
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> CreateStaff([FromBody] CreateStaffDto createStaffDto)
        {
            try
            {
                var staff = await _staffService.AddStaffAsync(createStaffDto);
                return CreatedAtAction(nameof(GetStaffById), new { id = staff.Id }, staff);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating staff");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo nhan viên mới viên");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StaffDto>> UpdateStaff(Guid id, [FromBody] UpdateStaffDto updateStaffDto)
        {
            try
            {
                var staff = await _staffService.UpdateStaffAsync(id, updateStaffDto);
                return staff;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating staff with ID {StaffId}", id);
                return StatusCode(500, "Đã xảy ra lỗi khi cập nhật nhân viên");
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult> UpdateStaffStatus(Guid id, [FromBody] StaffStatusUpdateDto statusUpdateDto)
        {
            try
            {
                var result = await _staffService.UpdateStaffStatusAsync(id, statusUpdateDto);
                if (result)
                    return NoContent();
                return NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for staff with ID {StaffId}", id);
                return StatusCode(500, "Đã xảy ra lỗi khi lấy cập nhật trạng thái nhân viên");
            }
        }

        [HttpPost("{id}/departments")]
        public async Task<ActionResult> AssignDepartment(Guid id, [FromBody] StaffDepartmentDto departmentDto)
        {
            try
            {
                var result = await _staffService.AssignDepartmentAsync(id, departmentDto);
                if (result)
                    return NoContent();
                return NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning department for staff with ID {StaffId}", id);
                return StatusCode(500, "Đã xảy ra lỗi khi thêm cở sỏ và bộ môn chuyên ngành");
            }
        }
        [HttpDelete("{id}/departments/{facilityId}")]
        public async Task<ActionResult> RemoveDepartment(Guid id, Guid facilityId)
        {
            try
            {
                var result = await _staffService.RemoveDepartmentAsync(id, facilityId);
                if (result)
                    return NoContent();
                return NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing department for staff with ID {StaffId}", id);
                return StatusCode(500, "Đã xảy ra lỗi khi xóa cơ sở nhân viên");
            }
        }

        [HttpGet("import-template")]
        public async Task<ActionResult> GetImportTemplate()
        {
            try
            {
                var templateBytes = await _staffService.GetImportTemplateAsync();
                return File(templateBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeImportTemplate.xlsx");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating import template");
                return StatusCode(500, "Đã xảy ra lỗi khi tạo mẫu nhập");
            }
        }
        //[HttpGet("export-excel")]
        //public async Task<ActionResult> GetExportExcel([FromQuery] string search = "")
        //{ 
        //    try
        //    {
        //        var excelBytes = await _staffService.GetExportExcel(search);
        //        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StaffsExport.xlsx");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error exporting staff data to Excel");
        //        return StatusCode(500, new { Message = "An error occurred while exporting staff data to Excel", Details = ex.Message });
        //    }
        //}

        [HttpPost("import")]
        public async Task<ActionResult<ImportResultDto>> ImportStaff([FromBody] List<StaffImportDto> staffs, [FromQuery] string userName)
        {
            try
            {
                if (staffs == null || !staffs.Any())
                {
                    return BadRequest(new ImportResultDto
                    {
                        Success = false,
                        ErrorMessage = "Danh sách nhân viên không hợp lệ hoặc rỗng.",
                        SuccessCount = 0,
                        FailCount = 0,
                        Errors = new List<string>()
                    });
                }

                userName ??= User.Identity?.Name ?? "System";
                var result = await _staffService.ImportStaffAsync(staffs, userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing staff data");
                return StatusCode(500, new ImportResultDto
                {
                    Success = false,
                    ErrorMessage = $"An error occurred while importing staff data: {ex.Message}",
                    SuccessCount = 0,
                    FailCount = 0,
                    Errors = new List<string>()
                });
            }
        }

        [HttpGet("import-history")]
        public async Task<ActionResult<List<ImportLogDto>>> GetImportHistory()
        {
            try
            {
                var history = await _staffService.GetImportHistoryAsync();
                return history;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving import history");
                return StatusCode(500, "An error occurred while retrieving import history");
            }
        }

        [HttpGet("import-history/{importId}")]
        public async Task<ActionResult<ImportLogDetailDto>> GetImportDetails(Guid importId)
        {
            try
            {
                var details = await _staffService.GetImportLogDetailsAsync(importId);
                return details;
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving import details for ID {ImportId}", importId);
                return StatusCode(500, "An error occurred while retrieving import details");
            }
        }
        [HttpPost("read-excel")]
        public async Task<ActionResult<List<StaffImportDto>>> ReadExcel([FromForm] IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File không hợp lệ hoặc rỗng.");
                }

                using var stream = file.OpenReadStream();
                var excelService = new ExcelService();
                var staffs = excelService.ReadStaffFromExcel(stream);
                return Ok(staffs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading Excel file");
                return StatusCode(500, $"Lỗi khi đọc file Excel: {ex.Message}");
            }
        }
    }
}
