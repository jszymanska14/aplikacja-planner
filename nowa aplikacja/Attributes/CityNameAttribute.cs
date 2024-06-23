using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace nowa_aplikacja.Attributes
{
   
    public class CityNameAttribute : ValidationAttribute
    {
        // Metoda walidacyjna sprawdzająca poprawność wartości
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                // Zwrócenie błędu walidacji, jeśli wartość jest null lub pusta
                return new ValidationResult("City name is required.");
            }

            var cityName = value.ToString();
            // Wyrażenie regularne sprawdzające, czy nazwa miasta zawiera tylko litery i spacje
            var regex = new Regex(@"^[a-zA-Z\s]+$");

            // Sprawdzenie, czy wartość spełnia wyrażenie regularne
            if (!regex.IsMatch(cityName))
            {
                // Zwrócenie błędu walidacji, jeśli nazwa miasta zawiera inne znaki niż litery i spacje
                return new ValidationResult("City name must contain only letters and spaces.");
            }

            return ValidationResult.Success;
        }
    }
}
