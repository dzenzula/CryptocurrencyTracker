using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyTracker.Library.Helpers
{
    public class ClientHelper
    {
        public static HttpClient GetClient(string baseAddress)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            return client;
        }

        public const string CoinCap = "https://api.coincap.io/v2/";
    }
}
