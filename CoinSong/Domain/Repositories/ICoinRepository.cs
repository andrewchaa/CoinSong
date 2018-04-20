using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Domain.Models;

namespace CoinSong.Domain.Repositories
{
    public interface ICoinRepository
    {
        Task<SnapshotPrice> GetPriceAsync(Coin coin);
        Task<IEnumerable<SnapshotPrice>> GetPricesAsync();
    }
}