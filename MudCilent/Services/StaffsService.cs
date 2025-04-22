namespace MudCilent.Services
{
    public class StaffsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "Staffs";

        public StaffsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StaffDto>> GetAllStaffAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StaffDto>>(_baseUrl) ?? new List<StaffDto>();
        }

        public async Task<StaffDto> GetStaffByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StaffDto>() ?? throw new KeyNotFoundException("Staff not found");
        }

        public async Task<StaffDto> AddStaffAsync(CreateStaffDto createStaffDto)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, createStaffDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StaffDto>() ?? throw new Exception("Failed to create staff");
        }

        public async Task<StaffDto> UpdateStaffAsync(Guid id, UpdateStaffDto updateStaffDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", updateStaffDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StaffDto>() ?? throw new KeyNotFoundException("Staff not found");
        }

        public async Task<bool> UpdateStaffStatusAsync(Guid id, StaffStatusUpdateDto statusUpdateDto)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{_baseUrl}/{id}/status", statusUpdateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AssignDepartmentAsync(Guid id, StaffDepartmentDto departmentDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{id}/departments", departmentDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveDepartmentAsync(Guid id, Guid facilityId)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}/departments/{facilityId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<byte[]> GetImportTemplateAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/import-template");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<ImportResultDto> ImportStaffAsync(Stream fileStream, string userName)
        {
            using var content = new MultipartFormDataContent();
            using var streamContent = new StreamContent(fileStream);
            content.Add(streamContent, "file", "EmployeeImport.xlsx");

            var response = await _httpClient.PostAsync($"{_baseUrl}/import", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ImportResultDto>() ?? throw new Exception("Failed to import staff");
        }

        public async Task<List<ImportLogDto>> GetImportHistoryAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ImportLogDto>>($"{_baseUrl}/import-history") ?? new List<ImportLogDto>();
        }

        public async Task<ImportLogDetailDto> GetImportLogDetailsAsync(Guid importId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/import-history/{importId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ImportLogDetailDto>() ?? throw new KeyNotFoundException("Import log not found");
        }
    }
}
