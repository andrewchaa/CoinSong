namespace GdaxApi.Models.Realtime
{
    public class RealtimeOpen : RealtimeMessage
    {
        public string order_id { get; set; }
        public decimal remaining_size { get; set; }
    }
}