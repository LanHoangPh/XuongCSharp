namespace MudCilent.Services
{
    public class LocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "Locations";

        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FacilityDto>> GetLocationsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<FacilityDto>>(_baseUrl) ?? new List<FacilityDto>();
        }
    }
}
