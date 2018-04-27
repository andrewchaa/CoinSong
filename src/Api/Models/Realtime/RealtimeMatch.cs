using System;

namespace GdaxApi.Models.Realtime
{
    public class RealtimeMatch : RealtimeMessage
    {
        public decimal trade_id { get; set; }
        public string maker_order_id { get; set; }
        public string taker_order_id { get; set; }
        public DateTime time { get; set; }
    }
}