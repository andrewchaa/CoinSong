using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Domain.Models;
using CoinSong.Infrastructure;
using Dapper;
using GdaxApi.Authentication;
using GdaxApi.Clients;
using GdaxApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Sulmo;

namespace CoinSong.Domain.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger<PriceRepository> _logger;
        private readonly RequestAuthenticator _authenticator;

        public PriceRepository(IOptions<AppSettings> options, ILogger<PriceRepository> logger)
        {
            _appSettings = options.Value;
            _logger = logger;
            _authenticator = new RequestAuthenticator(_appSettings.ApiKey, _appSettings.Passphrase, _appSettings.Secret);
        }

        public async Task<IList<Candle>> GetDailyRates(Coin coin, int days)
        {
            var client = new ProductClient(_appSettings.ProductionBaseUri, _authenticator);
            var productId = $"{coin.ToUpperString()}-EUR";

            return await client.GetHistoricRatesAsync(productId, DateTimeOffset.UtcNow.AddDays(-1 * (days + 1)),
                DateTimeOffset.UtcNow, TimeSpan.FromDays(1));

        }

        public async Task<ProductPrice> GetCurrentPrice(string productId)
        {
            var client = new ProductClient(_appSettings.ProductionBaseUri, _authenticator);
            var response = await client.GetProductTickerAsync(productId);
            return response.Value;
        }

        public async Task<int> SaveAsync(SnapshotPrice price)
        {            
            using (var conn = new MySqlConnection(_appSettings.CoinSongDb))
            {
                await conn.OpenAsync();
                var priceId = await conn.ExecuteAsync($@"
                    INSERT INTO Prices (
                        Coin,
                        Price,
                        Currency,
                        Ask,
                        Bid,
                        Size,
                        Time)
                    VALUES (
                        @Coin,
                        @Price,
                        @Currency,
                        @Ask,
                        @Bid,
                        @Size,
                        @Time
                    )
                ", new
                {
                    Coin = price.Coin.ToUpperString(),
                    price.Price,
                    Currency = price.Currency.ToUpperString(),
                    price.Ask,
                    price.Bid,
                    price.Size,
                    price.Time
                });

                return priceId;
            }
        }
    }
}