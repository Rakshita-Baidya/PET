using PET.Models;

namespace PET.Interfaces
{
    public interface ICurrency
    {
        // Load all currencies from a JSON file
        Task<List<Currencies>> LoadAllCurrenciesAsync();
    }
}
