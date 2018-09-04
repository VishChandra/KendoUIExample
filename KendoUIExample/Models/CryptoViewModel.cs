using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmonaut.Attributes;
using Cosmonaut.Extensions;

namespace KendoUIExample.Models
{
    [CosmosCollection("cryptocurrencies")]
    public class CryptoViewModel
    {
        [CosmosPartitionKey]
        public string Id { get; set; }
        public string cryptoName { get; set; }
        public string valueUSD { get; set; }
        public string valueBTC { get; set; }
        public string valueETH { get; set; }
        public string MarketCap { get; set; }
        public string Volume { get; set; }
        public string CirculatingSupply { get; set; }
        public string TotalSupply { get; set; }
        

    }
}
