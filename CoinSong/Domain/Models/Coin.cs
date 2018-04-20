using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinSong.Domain.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Coin
    {
        BTC,
        ETH,
        LTC
    }
}