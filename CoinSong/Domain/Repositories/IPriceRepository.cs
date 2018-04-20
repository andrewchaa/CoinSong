using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Domain.Models;
using GdaxApi.Models;

namespace CoinSong.Domain.Repositories
{
    public interface IPriceRepository
    {
        Task<int> SaveAsync(SnapshotPrice price);
        Task<IList<Candle>> GetDailyRates(Coin coin, int days);
        Task<ProductPrice> GetCurrentPrice(string productId);
    }
}