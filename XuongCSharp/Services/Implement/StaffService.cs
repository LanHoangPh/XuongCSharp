using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using XuongCSharp.DataAccess.Entities;
using XuongCSharp.Import;
using ValidationException = FluentValidation.ValidationException;

namespace XuongCSharp.Services.Inteplement
{
    public class StaffService : IStaffService
    {
        private readonly AppDbContext _context;
        private readonly IValidator<CreateStaffDto> _createValidator;
        private readonly IValidator<UpdateStaffDto> _updateValidator;
        private readonly IMapper _mapper;
        private readonly ILogger<StaffService> _logger;

        public StaffService(
            AppDbContext context,
            IValidator<CreateStaffDto> createValidator,
            IValidator<UpdateStaffDto> updateValidator,
            IMapper mapper,
            ILogger<StaffService> logger)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<StaffDto> GetStaffByIdAsync(Guid id)
        {
            var staff = await _context.Staffs
                .Include(s => s.StaffMajorFacilities)
                    .ThenInclude(smf => smf.MajorFacility)
                        .ThenInclude(mf => mf!.Major)
                .Include(s => s.StaffMajorFacilities)
                    .ThenInclude(smf => smf.MajorFacility)
                        .ThenInclude(mf => mf!.DepartmentFacility)
                            .ThenInclude(df => df!.Department)
                .Include(s => s.StaffMajorFacilities)
                    .ThenInclude(smf => smf.MajorFacility)
                        .ThenInclude(mf => mf!.DepartmentFacility)
                            .ThenInclude(df => df!.Facility)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (staff == null)
                throw new KeyNotFoundException($"Staff với id {id} ko khả dụng");


            return _mapper.Map<StaffDto>(staff);
        }

        public async Task<StaffDto> AddStaffAsync(CreateStaffDto createStaffDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createStaffDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Check uniqueness
            var isCodeUnique = !await _context.Staffs.AnyAsync(s => s.StaffCode == createStaffDto.StaffCode);
            var isFptEmailUnique = !await _context.Staffs.AnyAsync(s => s.AccountFpt == createStaffDto.AccountFpt);
            var isFeEmailUnique = !await _context.Staffs.AnyAsync(s => s.AccountFe == createStaffDto.AccountFe);

            if (!isCodeUnique)
                throw new ValidationException("Mã nhân viên phải là duy nhất");
            if (!isFptEmailUnique)
                throw new ValidationException("FPT email phải là duy nhất");
            if (!isFeEmailUnique)
                throw new ValidationException("FE email phải là duy nhất");

            var staff = _mapper.Map<Staff>(createStaffDto);
            staff.Id = Guid.NewGuid();
            staff.CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await _context.Staffs.AddAsync(staff);
            await _context.SaveChangesAsync();

            return _mapper.Map<StaffDto>(staff);
        }

        public async Task<IEnumerable<StaffDto>> GetAllStaffAsync()
        {
            var staffList = await _context.Staffs
            .Include(s => s.StaffMajorFacilities)
                .ThenInclude(smf => smf.MajorFacility)
                    .ThenInclude(mf => mf!.Major)
            .Include(s => s.StaffMajorFacilities)
                .ThenInclude(smf => smf.MajorFacility)
                    .ThenInclude(mf => mf!.DepartmentFacility)
                        .ThenInclude(df => df!.Department)
            .Include(s => s.StaffMajorFacilities)
                .ThenInclude(smf => smf.MajorFacility)
                    .ThenInclude(mf => mf!.DepartmentFacility)
                        .ThenInclude(df => df!.Facility)
            .OrderByDescending(s => s.CreatedDate).ToListAsync();

            return _mapper.Map<IEnumerable<StaffDto>>(staffList);
        }

        public async Task<StaffDto> UpdateStaffAsync(Guid id, UpdateStaffDto updateStaffDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateStaffDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var staff = await _context.Staffs
                .Include(s => s.StaffMajorFacilities)
                    .ThenInclude(smf => smf.MajorFacility)
                        .ThenInclude(mf => mf!.Major)
                .Include(s => s.StaffMajorFacilities)
                    .ThenInclude(smf => smf.MajorFacility)
                        .ThenInclude(mf => mf!.DepartmentFacility)
                            .ThenInclude(df => df!.Department)
                .Include(s => s.StaffMajorFacilities)
                    .ThenInclude(smf => smf.MajorFacility)
                        .ThenInclude(mf => mf!.DepartmentFacility)
                            .ThenInclude(df => df!.Facility)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (staff == null)
                throw new KeyNotFoundException($"Staff với ID {id} ko khả dụng");

            var isFptEmailUnique = !await _context.Staffs
                .AnyAsync(s => s.Id != id && s.AccountFpt == updateStaffDto.AccountFpt);

            var isFeEmailUnique = !await _context.Staffs
                .AnyAsync(s => s.Id != id && s.AccountFe == updateStaffDto.AccountFe);
            var isFeStaffCodeUnique = !await _context.Staffs
                .AnyAsync(s => s.Id != id && s.StaffCode == updateStaffDto.StaffCode);

            if (!isFptEmailUnique)
                throw new ValidationException("FPT email Không đc bỏ trống");

            if (!isFeEmailUnique)
                throw new ValidationException("FE email Không đc bỏ trống");
            if (!isFeStaffCodeUnique)
                throw new ValidationException("FE email Không đc bỏ trống");

            staff.Name = updateStaffDto.Name;
            staff.StaffCode = updateStaffDto.StaffCode;
            staff.AccountFpt = updateStaffDto.AccountFpt;
            staff.AccountFe = updateStaffDto.AccountFe;
            staff.LastModifiedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();

            return _mapper.Map<StaffDto>(staff);
        }

        public async Task<bool> UpdateStaffStatusAsync(Guid id, StaffStatusUpdateDto statusUpdateDto)
        {
            var staff = await _context.Staffs.FindAsync(id);

            if (staff == null)
                throw new KeyNotFoundException($"Staff với ID {id} ko tìm thấy");

            staff.Status = statusUpdateDto.Status;
            staff.LastModifiedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            _context.Staffs.Update(staff);

            var statusLog = new StaffStatusLog
            {
                Id = Guid.NewGuid(),
                StaffId = id,
                OldStatus = staff.Status == 1 ? (byte)0 : (byte)1, 
                NewStatus = (byte)staff.Status,
                Reason = statusUpdateDto.Reason,
                LoggedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            await _context.StaffStatusLogs.AddAsync(statusLog);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AssignDepartmentAsync(Guid staffId, StaffDepartmentDto departmentDto)
        {
            var staff = await _context.Staffs.FindAsync(staffId);

            if (staff == null)
                throw new KeyNotFoundException($"Staff với ID {staffId} not found");

            var facility = await _context.Facilities.FindAsync(departmentDto.FacilityId);
            var department = await _context.Departments.FindAsync(departmentDto.DepartmentId);
            var major = await _context.Majors.FindAsync(departmentDto.MajorId);

            if (facility == null)
                throw new KeyNotFoundException($"Facility với ID {departmentDto.FacilityId} ko khả dụng");

            if (department == null)
                throw new KeyNotFoundException($"Department với ID {departmentDto.DepartmentId} ko khả dụng");

            if (major == null)
                throw new KeyNotFoundException($"Specialization với ID {departmentDto.MajorId} ko khả dụng");

            var existingAssignments = await _context.StaffMajorFacilities
                .Include(smf => smf.MajorFacility)
                    .ThenInclude(mf => mf!.DepartmentFacility)
                .Where(smf =>
                    smf.IdStaff == staffId &&
                    smf.MajorFacility!.DepartmentFacility!.IdFacility == departmentDto.FacilityId)
                .ToListAsync();

            if (existingAssignments.Any())
            {
                _context.StaffMajorFacilities.RemoveRange(existingAssignments);
                await _context.SaveChangesAsync();
            }

            var departmentFacility = await _context.DepartmentFacilities
                .FirstOrDefaultAsync(df =>
                    df.IdDepartment == departmentDto.DepartmentId &&
                    df.IdFacility == departmentDto.FacilityId);

            if (departmentFacility == null)
            {
                departmentFacility = new DepartmentFacility
                {
                    Id = Guid.NewGuid(),
                    IdDepartment = departmentDto.DepartmentId,
                    IdFacility = departmentDto.FacilityId,
                    Status = 1,
                    CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                await _context.DepartmentFacilities.AddAsync(departmentFacility);
                await _context.SaveChangesAsync();
            }

            var majorFacility = await _context.MajorFacilities
                .FirstOrDefaultAsync(mf =>
                    mf.IdDepartmentFacility == departmentFacility.Id &&
                    mf.IdMajor == departmentDto.MajorId);

            if (majorFacility == null)
            {
                majorFacility = new MajorFacility
                {
                    Id = Guid.NewGuid(),
                    IdDepartmentFacility = departmentFacility.Id,
                    IdMajor = departmentDto.MajorId,
                    Status = 1,
                    CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                await _context.MajorFacilities.AddAsync(majorFacility);
                await _context.SaveChangesAsync();
            }

            var staffMajorFacility = new StaffMajorFacility
            {
                Id = Guid.NewGuid(),
                IdStaff = staffId,
                IdMajorFacility = majorFacility.Id,
                Status = 1,
                CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            await _context.StaffMajorFacilities.AddAsync(staffMajorFacility);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveDepartmentAsync(Guid staffId, Guid facilityId)
        {
            var staff = await _context.Staffs.FindAsync(staffId);

            if (staff == null)
                throw new KeyNotFoundException($"Staff với ID {staffId} ko khả dụng");

            var staffAssignments = await _context.StaffMajorFacilities
                .Include(smf => smf.MajorFacility)
                    .ThenInclude(mf => mf!.DepartmentFacility)
                .Where(smf =>
                    smf.IdStaff == staffId &&
                    smf.MajorFacility!.DepartmentFacility!.IdFacility == facilityId)
                .ToListAsync();

            if (!staffAssignments.Any())
                return false;

            _context.StaffMajorFacilities.RemoveRange(staffAssignments);
            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<byte[]> GetImportTemplateAsync()
        {
            var excelService = new ExcelService();
            return excelService.GenerateImportTemplate();
        }

        public async Task<ImportResultDto> ImportStaffAsync(List<StaffImportDto> staffs, string performedBy)
        {
            if (staffs == null || !staffs.Any())
            {
                return new ImportResultDto
                {
                    Success = false,
                    ErrorMessage = "Danh sách nhân viên không hợp lệ hoặc rỗng.",
                    SuccessCount = 0,
                    FailCount = 0,
                    Errors = new List<string>()
                };
            }

            if (string.IsNullOrEmpty(performedBy))
            {
                return new ImportResultDto
                {
                    Success = false,
                    ErrorMessage = "Tên người thực hiện không được để trống.",
                    SuccessCount = 0,
                    FailCount = 0,
                    Errors = new List<string>()
                };
            }

            var importLog = new ImportLog
            {
                Id = Guid.NewGuid(),
                PerformedBy = performedBy,
                ImportDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                SuccessCount = 0,
                FailCount = 0,
                ImportLogDetails = new List<ImportLogDetail>()
            };

            var successCount = 0;
            var failCount = 0;
            var errors = new List<string>();

            foreach (var staffImport in staffs)
            {
                try
                {
                    var validationErrors = ValidateStaffImport(staffImport);
                    if (validationErrors.Any())
                    {
                        failCount++;
                        var errorDetail = new ImportLogDetail
                        {
                            Id = Guid.NewGuid(),
                            ImportLogId = importLog.Id,
                            RowData = JsonSerializer.Serialize(staffImport),
                            ErrorMessage = string.Join("; ", validationErrors)
                        };
                        importLog.ImportLogDetails.Add(errorDetail);
                        errors.Add($"Error with staff code {staffImport.StaffCode}: {string.Join("; ", validationErrors)}");
                        continue;
                    }

                    var existingStaff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffCode == staffImport.StaffCode);

                    if (existingStaff == null)
                    {
                        var newStaff = new Staff
                        {
                            Id = Guid.NewGuid(),
                            StaffCode = staffImport.StaffCode,
                            Name = staffImport.Name,
                            AccountFpt = staffImport.AccountFpt,
                            AccountFe = staffImport.AccountFe,
                            Status = staffImport.Status,
                            CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                            LastModifiedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                        };

                        await _context.Staffs.AddAsync(newStaff);
                        await AssignDepartmentAndMajor(newStaff.Id, staffImport);
                        successCount++;
                    }
                    else
                    {
                        existingStaff.Name = staffImport.Name;
                        existingStaff.AccountFpt = staffImport.AccountFpt;
                        existingStaff.AccountFe = staffImport.AccountFe;
                        existingStaff.Status = staffImport.Status;
                        existingStaff.LastModifiedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

                        _context.Staffs.Update(existingStaff);
                        await AssignDepartmentAndMajor(existingStaff.Id, staffImport);
                        successCount++;
                    }
                }
                catch (Exception ex)
                {
                    failCount++;
                    var errorDetail = new ImportLogDetail
                    {
                        Id = Guid.NewGuid(),
                        ImportLogId = importLog.Id,
                        RowData = JsonSerializer.Serialize(staffImport),
                        ErrorMessage = ex.Message
                    };
                    importLog.ImportLogDetails.Add(errorDetail);
                    errors.Add($"Error with staff code {staffImport.StaffCode}: {ex.Message}");
                }
            }

            importLog.SuccessCount = successCount;
            importLog.FailCount = failCount;

            await _context.ImportLogs.AddAsync(importLog);
            await _context.SaveChangesAsync();

            return new ImportResultDto
            {
                Success = true,
                ImportId = importLog.Id,
                SuccessCount = successCount,
                FailCount = failCount,
                Errors = errors,
                ErrorMessage = errors.Any() ? "Một số nhân viên không được import do lỗi dữ liệu." : null
            };
        }

        private async Task AssignDepartmentAndMajor(Guid staffId, StaffImportDto staffImport)
        {
            var facility = await _context.Facilities
        .FirstOrDefaultAsync(f => f.Code == staffImport.LocationCode);

            if (facility == null)
                throw new KeyNotFoundException($"Location với code {staffImport.LocationCode} ko khả dụng");

            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.Code == staffImport.DepartmentCode);

            if (department == null)
                throw new KeyNotFoundException($"Department với code {staffImport.DepartmentCode} ko khả dụng");

            var major = await _context.Majors
                .FirstOrDefaultAsync(m => m.Code == staffImport.MajorCode);

            if (major == null)
                throw new KeyNotFoundException($"Specialization với code {staffImport.MajorCode} ko khả dụng");

            var existingAssignmentInFacility = await _context.StaffMajorFacilities
                        .Include(smf => smf.MajorFacility)
                            .ThenInclude(mf => mf.DepartmentFacility)
                        .FirstOrDefaultAsync(smf =>
                            smf.IdStaff == staffId &&
                            smf.MajorFacility.DepartmentFacility.IdFacility == facility.Id &&
                            smf.Status == 1);

            if (existingAssignmentInFacility != null)
            {
                throw new InvalidOperationException($"Staff đã được phân công vào một phòng ban và chuyên ngành tại cơ sở {staffImport.LocationCode}. Mỗi cơ sở chỉ được phép có một phòng ban và chuyên ngành.");
            }

            var departmentFacility = await _context.DepartmentFacilities
                .FirstOrDefaultAsync(df =>
                    df.IdDepartment == department.Id &&
                    df.IdFacility == facility.Id);

            if (departmentFacility == null)
            {
                departmentFacility = new DepartmentFacility
                {
                    Id = Guid.NewGuid(),
                    IdDepartment = department.Id,
                    IdFacility = facility.Id,
                    Status = 1,
                    CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                await _context.DepartmentFacilities.AddAsync(departmentFacility);
                await _context.SaveChangesAsync();
            }

            var majorFacility = await _context.MajorFacilities
                .FirstOrDefaultAsync(mf =>
                    mf.IdDepartmentFacility == departmentFacility.Id &&
                    mf.IdMajor == major.Id);

            if (majorFacility == null)
            {
                majorFacility = new MajorFacility
                {
                    Id = Guid.NewGuid(),
                    IdDepartmentFacility = departmentFacility.Id,
                    IdMajor = major.Id,
                    Status = 1,
                    CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                await _context.MajorFacilities.AddAsync(majorFacility);
                await _context.SaveChangesAsync();
            }

            var existingAssignment = await _context.StaffMajorFacilities
                .FirstOrDefaultAsync(smf =>
                    smf.IdStaff == staffId &&
                    smf.IdMajorFacility == majorFacility.Id);

            if (existingAssignment == null)
            {
                var staffMajorFacility = new StaffMajorFacility
                {
                    Id = Guid.NewGuid(),
                    IdStaff = staffId,
                    IdMajorFacility = majorFacility.Id,
                    Status = 1,
                    CreatedDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                };

                await _context.StaffMajorFacilities.AddAsync(staffMajorFacility);
                await _context.SaveChangesAsync();
            }
        }

        private List<string> ValidateStaffImport(StaffImportDto staffImport)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(staffImport.StaffCode))
                errors.Add("Mã nhân viên là bắt buộc");
            else if (staffImport.StaffCode.Length > 15)
                errors.Add("Mã nhân viên không được vượt quá 15 ký tự");

            if (string.IsNullOrWhiteSpace(staffImport.Name))
                errors.Add("Tên là bắt buộc");
            else if (staffImport.Name.Length > 100)
                errors.Add("Tên không được vượt quá 100 ký tự");

            if (string.IsNullOrWhiteSpace(staffImport.AccountFpt))
                errors.Add("Email FPT là bắt buộc");
            else if (staffImport.AccountFpt.Length > 100)
                errors.Add("Email FPT không được vượt quá 100 ký tự");
            else if (!Regex.IsMatch(staffImport.AccountFpt, @"^[a-zA-Z0-9._-]+@fpt\.edu\.vn$"))
                errors.Add("Email FPT phải kết thúc bằng @fpt.edu.vn");
            else if (staffImport.AccountFpt.Contains(" ") || ContainsVietnameseChars(staffImport.AccountFpt))
                errors.Add("Email FPT không được chứa khoảng trắng hoặc ký tự tiếng Việt");

            if (string.IsNullOrWhiteSpace(staffImport.AccountFe))
                errors.Add("Email FE là bắt buộc");
            else if (staffImport.AccountFe.Length > 100)
                errors.Add("Email FE không được vượt quá 100 ký tự");
            else if (!Regex.IsMatch(staffImport.AccountFe, @"^[a-zA-Z0-9._-]+@fe\.edu\.vn$"))
                errors.Add("Email FE phải kết thúc bằng @fe.edu.vn");
            else if (staffImport.AccountFe.Contains(" ") || ContainsVietnameseChars(staffImport.AccountFe))
                errors.Add("Email FE không được chứa khoảng trắng hoặc ký tự tiếng Việt");

            return errors;
        }

        private bool ContainsVietnameseChars(string value)
        {
            string vietnameseChars = "àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ";
            return value.ToLower().Any(c => vietnameseChars.Contains(c));
        }
                  
        public async Task<List<ImportLogDto>> GetImportHistoryAsync()
        {
            var importLogs = await _context.ImportLogs
                .OrderByDescending(log => log.ImportDate)
                .ToListAsync();

            return _mapper.Map<List<ImportLogDto>>(importLogs);
        }

        public async Task<ImportLogDetailDto> GetImportLogDetailsAsync(Guid importLogId)
        {
            var importLog = await _context.ImportLogs
                .Include(log => log.ImportLogDetails)
                .FirstOrDefaultAsync(log => log.Id == importLogId);

            if (importLog == null)
                throw new KeyNotFoundException($"Import log với ID {importLogId} ko khả dụng");

            return _mapper.Map<ImportLogDetailDto>(importLog);
        }

        //public Task<byte[]> GetExportExcel(List<StaffDto> staffDtos)
        //{
        //    var excelService = new ExcelService();
        //    return excelService.ExportStaffsToExcel(staffDtos);
        //}
    }
}
