namespace GdaxApi.Models.Realtime
{
    public class RealtimeMessage
    {
        public string type { get; set; }
        public long sequence { get; set; }
        public decimal? price { get; set; }
        public string side { get; set; }
    }
}