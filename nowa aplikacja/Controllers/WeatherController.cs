using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApi.Services;
using System;
using System.Net.Http;
using System.ComponentModel.DataAnnotations;
using nowa_aplikacja.Attributes;

namespace WeatherApi.Controllers
{
    [ApiController]
    // Ustawia domyślną trasę dla kontrolera
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        // Pole przechowujące serwis pogodowy
        private readonly WeatherService _weatherService;

        // Konstruktor przyjmujący serwis pogodowy jako zależność
        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        // Akcja zwracająca aktualną pogodę dla podanej lokalizacji
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather([FromQuery, Required, CityName] string location)
        {
            // Sprawdzenie, czy model jest poprawny
            if (!ModelState.IsValid)
            {
                // Zwrócenie odpowiedzi z błędami walidacji
                return BadRequest(ModelState);
            }

            try
            {
                // Pobranie aktualnych danych pogodowych dla podanej lokalizacji
                var weatherData = await _weatherService.GetCurrentWeatherAsync(location);
                // Zwrócenie danych pogodowych
                return Ok(weatherData);
            }
            catch (HttpRequestException httpRequestException)
            {
                // Logowanie błędu HTTP
                Console.WriteLine($"An HTTP request error occurred: {httpRequestException.Message}");
                // Zwrócenie odpowiedzi z kodem 503 i komunikatem o błędzie
                return StatusCode(503, "Error retrieving weather data. Please try again later.");
            }
            catch (Exception ex)
            {
                // Logowanie ogólnego błędu
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Zwrócenie odpowiedzi z kodem 500 i komunikatem o błędzie
                return StatusCode(500, "An internal server error occurred. Please try again later.");
            }
        }
    }
}
