namespace CoinSong.Infrastructure
{
    public class AppSettings
    {
        public string CoinSongDb { get; set; }
        public string ProductionBaseUri { get; set; }
        public string BaseUri { get; set; }
        public string ApiKey { get; set; }
        public string Secret { get; set; }
        public string Passphrase { get; set; }
    }
}