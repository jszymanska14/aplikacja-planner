using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    // Klasa serwisu pogodowego odpowiedzialna za pobieranie danych pogodowych z API
    public class WeatherService
    {
        // Prywatne pole przechowujące instancję HttpClient
        private readonly HttpClient _httpClient;

        // Klucz API do usługi OpenWeatherMap
        private readonly string _apiKey = "b77b457cf16fe9ef6d717626efa9a34f";

        // Konstruktor przyjmujący HttpClient jako zależność
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Metoda asynchroniczna do pobierania aktualnych danych pogodowych dla podanej lokalizacji
        public async Task<WeatherData> GetCurrentWeatherAsync(string location)
        {
            // Tworzenie URL z podaną lokalizacją i kluczem API
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}&units=metric";

            // Pobieranie odpowiedzi jako ciąg znaków
            var response = await _httpClient.GetStringAsync(url);
            // Deserializacja odpowiedzi JSON do obiektu WeatherApiResponse
            var data = JsonConvert.DeserializeObject<WeatherApiResponse>(response);

            // Tworzenie i zwracanie obiektu WeatherData na podstawie danych z API
            return new WeatherData
            {
                Location = data.Name,
                Temperature = data.Main.Temp,
                Condition = data.Weather[0].Description
            };
        }

        // Klasa pomocnicza do deserializacji odpowiedzi JSON z API
        private class WeatherApiResponse
        {
            public string Name { get; set; } // Nazwa lokalizacji
            public MainData Main { get; set; } // Dane główne (temperatura)
            public WeatherData[] Weather { get; set; } // Dane dotyczące warunków pogodowych

            // Klasa pomocnicza do przechowywania danych głównych (temperatura)
            public class MainData
            {
                public float Temp { get; set; } // Temperatura
            }

            // Klasa pomocnicza do przechowywania danych o warunkach pogodowych
            public class WeatherData
            {
                public string Description { get; set; } // Opis warunków pogodowych
            }
        }
    }
}
