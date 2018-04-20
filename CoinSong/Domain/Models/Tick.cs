using System;
using Newtonsoft.Json;

namespace CoinSong.Domain.Models
{
    public class Tick
    {
        [JsonProperty(PropertyName = "trade_id")]
        public int TradeId { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string Bid { get; set; }
        public string Ask { get; set; }
        public string Volume { get; set; }
        public DateTime Time { get; set; }
    }
}