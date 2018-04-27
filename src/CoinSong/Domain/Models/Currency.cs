using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoinSong.Api.Domain.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        BTC,
        EUR
    }
}