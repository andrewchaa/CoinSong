using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Api.Domain.Models;
using GdaxApi.Models;

namespace CoinSong.Api.Domain.Repositories
{
    public interface IPriceRepository
    {
        Task<int> SaveAsync(SnapshotPrice price);
        Task<IList<Candle>> GetDailyRates(Coin coin, int days);
        Task<ProductPrice> GetCurrentPrice(string productId);
    }
}