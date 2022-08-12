using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrencyTracker.Library.DTOs
{
    public class CoinCapHistory
    {
        public decimal PriceUsd { get; set; }
        public long Time { get; set; }
    }
}
