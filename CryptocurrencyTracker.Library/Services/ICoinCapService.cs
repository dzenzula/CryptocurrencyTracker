
using CryptocurrencyTracker.Library.Models;

namespace CryptocurrencyTracker.Library.Services
{
    public interface ICoinCapService
    {
        Task<List<BaseCryptoModel>> GetAllCoinsAsync();
        Task<List<CoinHistory>> GetCoinHistory(string id, string interval);
    }
}