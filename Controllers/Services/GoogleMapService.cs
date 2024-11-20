namespace WayFarer.Controllers.Services
{
    public class GoogleMapService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleMapService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMapsApiSettings:ApiKey"];
        }


        public string GetLocationUrl(string queryData)
        {
            string baseUrl = "https://www.google.com/maps/embed/v1/place";

            string query = "&q=" + queryData.Replace(" ", "+");

            return baseUrl + "?key=" + _apiKey + query;
        }
    }
}
