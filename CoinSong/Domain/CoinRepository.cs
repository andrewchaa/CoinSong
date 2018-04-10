using System.Collections.Generic;
using CoinSong.Domain.Models;

namespace CoinSong.Domain
{
    public class CoinRepository : ICoinRepository
    {
        public IEnumerable<Price> GetPrices(CoinType coinType)
        {
            return new List<Price>();
        }
    }

    public class Price
    {
        
    }

    public interface ICoinRepository
    {
    }
}