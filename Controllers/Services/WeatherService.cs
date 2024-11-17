using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace WayFarer.Controllers.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["WeatherApiSettings:ApiKey"];
        }

        public async Task<WeatherData> GetWeatherByCityNameAsync(string cityName)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&units=metric&appid={_apiKey}";

            var response = await _httpClient.GetStringAsync(url);
            var data = JObject.Parse(response);

            var weatherData = new WeatherData
            {
                Forecasts = data["list"].Select(item => new WeatherForecast
                {
                    Date = (DateTime)item["dt_txt"],
                    Temperature = (float)item["main"]["temp"],
                    WeatherDescription = (string)item["weather"][0]["description"],
                    Icon = $"http://openweathermap.org/img/wn/{item["weather"][0]["icon"]}.png"
                }).ToList()
            };

            return weatherData;
        }
    }

    public class WeatherData
    {
        public List<WeatherForecast> Forecasts { get; set; }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public float Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public string Icon { get; set; }
    }
}
