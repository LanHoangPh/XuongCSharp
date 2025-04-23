using Microsoft.AspNetCore.Components.Forms;

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
        public async Task<List<StaffImportDto>> ReadExcelFileAsync(IBrowserFile file)
        {
            try
            {
                if (file == null)
                {
                    throw new ArgumentException("File không hợp lệ hoặc rỗng.");
                }

                // Gửi file lên API
                using var content = new MultipartFormDataContent();
                using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10MB
                using var streamContent = new StreamContent(stream);
                content.Add(streamContent, "file", file.Name);

                var response = await _httpClient.PostAsync($"{_baseUrl}/read-excel", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Yêu cầu đọc file Excel thất bại. Mã trạng thái: {response.StatusCode}. Chi tiết: {errorContent}");
                }

                var staffs = await response.Content.ReadFromJsonAsync<List<StaffImportDto>>();
                if (staffs == null)
                {
                    throw new InvalidOperationException("Không thể đọc danh sách nhân viên từ server.");
                }

                return staffs;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Lỗi khi gọi API đọc file Excel: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi không xác định khi đọc file Excel: {ex.Message}", ex);
            }
        }
        public async Task<ImportResultDto> ImportStaffAsync(List<StaffImportDto> staffs, string userName)
        {
            try
            {
                if (staffs == null || !staffs.Any())
                {
                    throw new ArgumentException("Danh sách nhân viên không hợp lệ hoặc rỗng.");
                }

                if (string.IsNullOrEmpty(userName))
                {
                    throw new ArgumentException("Tên người dùng không được để trống.");
                }

                var requestUri = $"{_baseUrl}/import?userName={Uri.EscapeDataString(userName)}";
                var response = await _httpClient.PostAsJsonAsync(requestUri, staffs);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Yêu cầu import thất bại. Mã trạng thái: {response.StatusCode}. Chi tiết: {errorContent}");
                }

                var importResult = await response.Content.ReadFromJsonAsync<ImportResultDto>();
                if (importResult == null)
                {
                    throw new InvalidOperationException("Không thể đọc kết quả import từ server.");
                }

                return importResult;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Lỗi khi gọi API import: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi không xác định khi import nhân viên: {ex.Message}", ex);
            }
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
