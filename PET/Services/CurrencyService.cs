using PET.Interfaces;
using PET.Models;
using System.Text.Json;

namespace PET.Services
{
    public class CurrenyService : ICurrency
    {
        // Path to currencies.json file
        private readonly string currenciesFilePath = Path.Combine(AppContext.BaseDirectory, "Details", "Currency.json");

        // Method to load currencies from the JSON file
        public async Task<List<Currencies>> LoadAllCurrenciesAsync()
        {
            try
            {
                if (!File.Exists(currenciesFilePath))
                {
                    Console.WriteLine("Currency file not found.");
                    return new List<Currencies>();
                }

                var json = await File.ReadAllTextAsync(currenciesFilePath);
                //Console.WriteLine("File read successfully.");

                return JsonSerializer.Deserialize<List<Currencies>>(json) ?? new List<Currencies>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                return new List<Currencies>();

            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"I/O error while loading tags: {ioEx.Message}");
                return new List<Currencies>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading currencies: {ex.Message}");
                return new List<Currencies>();
            }
        }

    }
}
