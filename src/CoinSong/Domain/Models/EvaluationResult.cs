using System.Collections.Generic;
using GdaxApi.Models;

namespace CoinSong.Api.Domain.Models
{
    public class EvaluationResult
    {
        public bool Pass { get; }
        public IList<Candle> Candles { get; }
        public decimal CurrentPrice { get; }
        public decimal ThresholdPrice { get; }

        public EvaluationResult(IList<Candle> candles, decimal currentPrice, decimal thresholdPrice)
        {
            Candles = candles;
            CurrentPrice = currentPrice;
            ThresholdPrice = thresholdPrice;
            Pass = CurrentPrice > ThresholdPrice;
        }
    }
}