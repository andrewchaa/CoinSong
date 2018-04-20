using System;

namespace CoinSong.Domain.Models
{
    public class SnapshotPrice
    {
        public Coin Coin { get; }
        public decimal Price { get; }
        public Currency Currency { get;}
        public decimal Ask { get; }
        public decimal Bid { get; }
        public decimal Size { get; }
        public DateTime Time { get; }

        public SnapshotPrice(Coin coin, string price, Currency currency, string ask, string bid, string size, DateTime time)
        {
            Coin = coin;
            Price = decimal.Parse(price);
            Currency = currency;
            Ask = decimal.Parse(ask);
            Bid = decimal.Parse(bid);
            Size = decimal.Parse(size);
            Time = time;
        }
    }
}