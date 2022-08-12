using CryptocurrencyTracker.Library.Helpers;
using CryptocurrencyTracker.Library.DTOs;
using Newtonsoft.Json;
using CryptocurrencyTracker.Library.Models;

namespace CryptocurrencyTracker.Library.Services
{
    public class CoinCapService : ICoinCapService
    {
        private HttpClient _httpClient;
        public CoinCapService()
        {
            _httpClient = ClientHelper.GetClient(ClientHelper.CoinCap);
        }

        public async Task<List<BaseCryptoModel>> GetAllCoinsAsync()
        {
            var parsedCryptoList = new List<BaseCryptoModel>();
            HttpResponseMessage response = await _httpClient.GetAsync("assets").ConfigureAwait(false);
            var contentJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            contentJson = contentJson.Replace("null", "0");
            Coins content = JsonConvert.DeserializeObject<Coins>(contentJson);

            foreach (var coin in content.data)
            {
                parsedCryptoList.Add(new BaseCryptoModel
                {
                    Id = coin.Id,
                    Rank = coin.Rank,
                    Symbol = coin.Symbol,
                    Name = coin.Name,
                    PriceUsd = coin.PriceUsd,
                    MarketCapUsd = coin.MarketCapUsd,
                    Supply = coin.Supply,
                    MaxSupply = coin.MaxSupply,
                    ChangePercent24Hr = coin.ChangePercent24Hr,
                    VolumeUsd24Hr = coin.VolumeUsd24Hr,
                    Explorer = coin.Explorer
                });
            }

            return parsedCryptoList;
        }

        public async Task<BaseCryptoModel> GetCoinAsync(string coinCapId)
        {
            BaseCryptoModel coin = new BaseCryptoModel();
            return coin;
        }
        public async Task<List<CoinHistory>> GetCoinHistory(string id, string interval)
        {
            var parsedHistoryList = new List<CoinHistory>();
            HttpResponseMessage response = await _httpClient.GetAsync($"assets/{id}/history?interval={interval}").ConfigureAwait(false);
            var contentJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            contentJson = contentJson.Replace("null", "0");
            CoinHistories content = JsonConvert.DeserializeObject<CoinHistories>(contentJson);

            foreach (var coinHistory in content.data)
            {
                parsedHistoryList.Add(new CoinHistory
                {
                    PriceUsd = coinHistory.PriceUsd,
                    DateTime = DateTimeOffset.FromUnixTimeMilliseconds(coinHistory.Time).DateTime
                });
            }

            return parsedHistoryList;
        }
    }
}
