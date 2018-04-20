using System.Collections.Generic;
using System.Linq;
using CoinSong.Domain.Models;
using GdaxApi.Models;

namespace CoinSong.Domain.Strategies
{
    public class MovingAverageStrategy : IStrategy
    {
        private readonly IList<Candle> _candles;
        private readonly ProductPrice _currentPrice;

        public MovingAverageStrategy(IList<Candle> candles, 
            ProductPrice currentPrice
            )
        {
            _candles = candles;
            _currentPrice = currentPrice;
        }

        public EvaluationResult Execute()
        {
            var movingAverage = Calculate(_candles);

            return new EvaluationResult(
                _candles,
                _currentPrice.price,
                movingAverage);
        }

        public decimal Calculate(IList<Candle> candles)
        {
            return candles.Sum(c => c.Close) / candles.Count;
        }

    }
}