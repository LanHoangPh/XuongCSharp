namespace MudCilent.Services
{
    public class SpecializationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "Specializations";

        public SpecializationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MajorDto>> GetSpecializationsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<MajorDto>>(_baseUrl) ?? new List<MajorDto>();
        }

        public async Task<List<MajorDto>> GetSpecializationsByDepartmentAsync(Guid locationId, Guid departmentId)
        {
            return await _httpClient.GetFromJsonAsync<List<MajorDto>>($"{_baseUrl}/by-department/{locationId}/{departmentId}") ?? new List<MajorDto>();
        }
    }
}
