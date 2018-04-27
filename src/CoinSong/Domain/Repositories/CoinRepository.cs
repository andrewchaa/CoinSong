using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinSong.Api.Domain.Models;
using CoinSong.Api.Infrastructure;
using GdaxApi.Authentication;
using GdaxApi.Clients;
using GdaxApi.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using Sulmo;

namespace CoinSong.Api.Domain.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ILogger<CoinRepository> _logger;
        private readonly RestClient _client;
        private readonly GdaxOptions _gdaxOptions;
        private readonly RequestAuthenticator _authenticator;

        public CoinRepository(ILogger<CoinRepository> logger, IOptions<GdaxOptions> options)
        {
            _logger = logger;
            _gdaxOptions = options.Value;
            _client = new RestClient(_gdaxOptions.BaseUri);
            _authenticator = new RequestAuthenticator(_gdaxOptions.ApiKey, _gdaxOptions.Passphrase, _gdaxOptions.Secret);

        }

        public async Task<SnapshotPrice> GetPriceAsync(Coin coin)
        {
            var request = new RestRequest($"/products/{coin.ToLowerString()}-eur/ticker");
            var tick = await _client.GetTaskAsync<Tick>(request);
            
            return new SnapshotPrice(coin, tick.Price, Currency.EUR, tick.Ask, tick.Bid, tick.Size, tick.Time);
        }
        public async Task<IList<Candle>> GetDailyRates(Coin coin, int days)
        {
            var client = new ProductClient(_gdaxOptions.BaseUri, _authenticator);
            var productId = $"{coin.ToUpperString()}-EUR";

            return await client.GetHistoricRatesAsync(productId, DateTimeOffset.UtcNow.AddDays(-1 * (days + 1)),
                DateTimeOffset.UtcNow, TimeSpan.FromDays(1));

        }
    }
}