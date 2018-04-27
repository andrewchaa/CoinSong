using System.Collections.Generic;

namespace GdaxApi.Models
{
    public class OrderBook
    {
        public long sequence { get; set; }
        public IEnumerable<string[]> bids { get; set; }
        public IEnumerable<string[]> asks { get; set; }
    }
}