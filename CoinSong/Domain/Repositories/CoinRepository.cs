using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinSong.Domain.Models;
using CoinSong.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using Sulmo;

namespace CoinSong.Domain.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly ILogger<CoinRepository> _logger;
        private readonly RestClient _client;
        private AppSettings _appSettings;

        public CoinRepository(ILogger<CoinRepository> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _appSettings = options.Value;
            _client = new RestClient(_appSettings.BaseUri);
        }
        
        public async Task<SnapshotPrice> GetPriceAsync(Coin coin)
        {
            var request = new RestRequest($"/products/{coin.ToLowerString()}-eur/ticker");
            var tick = await _client.GetTaskAsync<Tick>(request);
            
            return new SnapshotPrice(coin, tick.Price, Currency.EUR, tick.Ask, tick.Bid, tick.Size, tick.Time);
        }

        public async Task<IEnumerable<SnapshotPrice>> GetPricesAsync()
        {
            var prices = new List<SnapshotPrice>();
            
            foreach (var coinType in Enum.GetValues(typeof(Coin)).Cast<Coin>())
            {
                var request = new RestRequest($"/products/{coinType.ToLowerString()}-eur/ticker");
                var tick = await _client.GetTaskAsync<Tick>(request);

                _logger.LogInformation($"Price - {tick.Price}, Ask - {tick.Ask}, Bid - {tick.Bid}");
                prices.Add(new SnapshotPrice(coinType, tick.Price, Currency.EUR, tick.Ask, tick.Bid, tick.Size, tick.Time));
            }

            return prices;
        }
    }
}