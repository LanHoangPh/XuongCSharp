namespace XuongCSharp.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetAllStaffAsync();
        Task<StaffDto> GetStaffByIdAsync(Guid id);
        Task<StaffDto> AddStaffAsync(CreateStaffDto createStaffDto);
        Task<StaffDto> UpdateStaffAsync(Guid id, UpdateStaffDto updateStaffDto);
        Task<bool> UpdateStaffStatusAsync(Guid id, StaffStatusUpdateDto statusUpdateDto);
        Task<bool> AssignDepartmentAsync(Guid staffId, StaffDepartmentDto departmentDto);
        Task<bool> RemoveDepartmentAsync(Guid staffId, Guid facilityId);
        Task<byte[]> GetImportTemplateAsync();
        //Task<byte[]> GetExportExcel(List<StaffDto> staffDtos);
        Task<ImportResultDto> ImportStaffAsync(List<StaffImportDto> staffs, string performedBy);
        Task<List<ImportLogDto>> GetImportHistoryAsync();
        Task<ImportLogDetailDto> GetImportLogDetailsAsync(Guid importLogId);
    }
}


