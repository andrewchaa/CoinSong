using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Api.Domain.Models;
using GdaxApi.Models;

namespace CoinSong.Api.Domain.Repositories
{
    public interface ICoinRepository
    {
        Task<SnapshotPrice> GetPriceAsync(Coin coin);
        Task<IList<Candle>> GetDailyRates(Coin coin, int days);
    }
}