using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApi.Services;
using WeatherApi.Models;
using System;

namespace WeatherApi.Controllers
{
    // Klasa definiująca komponent widoku dla pogody
    public class WeatherViewComponent : ViewComponent
    {
        // Pole przechowujące serwis pogodowy
        private readonly WeatherService _weatherService;

        // Konstruktor przyjmujący serwis pogodowy jako zależność
        public WeatherViewComponent(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        // Metoda asynchroniczna wywoływana przez komponent widoku
        public async Task<IViewComponentResult> InvokeAsync(string location = "Warsaw")
        {
            try
            {
                // Pobranie aktualnych danych pogodowych dla podanej lokalizacji
                var weatherData = await _weatherService.GetCurrentWeatherAsync(location);
                // Zwrócenie widoku z danymi pogodowymi
                return View(weatherData);
            }
            catch (HttpRequestException httpRequestException)
            {
                // Logowanie błędu HTTP
                Console.WriteLine($"An HTTP request error occurred: {httpRequestException.Message}");
                
                // Zwrócenie widoku z odpowiednim komunikatem o błędzie
                return View("Error", "Error retrieving weather data. Please try again later.");
            }
            catch (Exception ex)
            {
                // Logowanie ogólnego błędu
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Zwrócenie widoku z ogólnym komunikatem o błędzie
                return View("Error", "An internal server error occurred. Please try again later.");
            }
        }
    }
}
