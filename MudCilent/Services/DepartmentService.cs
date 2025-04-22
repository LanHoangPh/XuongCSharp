namespace MudCilent.Services
{
    public class DepartmentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "Departments";

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DepartmentDto>> GetDepartmentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DepartmentDto>>(_baseUrl) ?? new List<DepartmentDto>();
        }

        public async Task<List<DepartmentDto>> GetDepartmentsByLocationAsync(Guid locationId)
        {
            return await _httpClient.GetFromJsonAsync<List<DepartmentDto>>($"{_baseUrl}/by-location/{locationId}") ?? new List<DepartmentDto>();
        }
    }
}
