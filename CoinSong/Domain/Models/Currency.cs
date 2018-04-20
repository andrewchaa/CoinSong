using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinSong.Domain.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        BTC,
        EUR
    }
}