using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Api.Domain.Models;
using CoinSong.Api.Infrastructure;
using Dapper;
using GdaxApi.Authentication;
using GdaxApi.Clients;
using GdaxApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Sulmo;

namespace CoinSong.Api.Domain.Repositories
{
    public class RatesRepository : IRatesRepository
    {
        private readonly GdaxOptions _gdaxOptions;
        private readonly ILogger<RatesRepository> _logger;
        private readonly RequestAuthenticator _authenticator;

        public RatesRepository(IOptions<GdaxOptions> options, ILogger<RatesRepository> logger)
        {
            _gdaxOptions = options.Value;
            _logger = logger;
            _authenticator = new RequestAuthenticator(_gdaxOptions.ApiKey, _gdaxOptions.Passphrase, _gdaxOptions.Secret);
        }

        public async Task<IList<Candle>> GetDailyRates(Coin coin, int days)
        {
            var client = new ProductClient(_gdaxOptions.BaseUri, _authenticator);
            var productId = $"{coin.ToUpperString()}-EUR";

            return await client.GetHistoricRatesAsync(productId, DateTimeOffset.UtcNow.AddDays(-1 * (days + 1)),
                DateTimeOffset.UtcNow, TimeSpan.FromDays(1));

        }

        public async Task<ProductPrice> GetCurrentPrice(string productId)
        {
            var client = new ProductClient(_gdaxOptions.BaseUri, _authenticator);
            var response = await client.GetProductTickerAsync(productId);
            return response.Value;
        }

        public async Task<int> SaveAsync(SnapshotPrice price)
        {
            return 0;
//            using (var conn = new MySqlConnection(_gdaxOptions.CoinSongDb))
//            {
//                await conn.OpenAsync();
//                var priceId = await conn.ExecuteAsync($@"
//                    INSERT INTO Prices (
//                        Coin,
//                        Price,
//                        Currency,
//                        Ask,
//                        Bid,
//                        Size,
//                        Time)
//                    VALUES (
//                        @Coin,
//                        @Price,
//                        @Currency,
//                        @Ask,
//                        @Bid,
//                        @Size,
//                        @Time
//                    )
//                ", new
//                {
//                    Coin = price.Coin.ToUpperString(),
//                    price.Price,
//                    Currency = price.Currency.ToUpperString(),
//                    price.Ask,
//                    price.Bid,
//                    price.Size,
//                    price.Time
//                });
//
//                return priceId;
//        }
        }
    }
}