using System;
using System.Collections;
using System.Threading.Tasks;
using CoinSong.Domain.Models;
using CoinSong.Domain.Repositories;
using CoinSong.Domain.Strategies;
using CoinSong.Infrastructure;
using GdaxApi.Authentication;
using GdaxApi.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sulmo;

namespace CoinSong.Controllers
{
    [Route("api/[controller]")]
    public class StrategiesController : Controller
    {
        private readonly ILogger<CoinsController> _logger;
        private readonly IPriceRepository _priceRepository;
        private readonly AppSettings _appSettings;
        private RequestAuthenticator _authenticator;

        public StrategiesController(ILogger<CoinsController> logger, 
            IOptions<AppSettings> options, 
            IPriceRepository priceRepository)
        {
            _logger = logger;
            _priceRepository = priceRepository;
            _appSettings = options.Value;
            _authenticator = new RequestAuthenticator(_appSettings.ApiKey, _appSettings.Passphrase, _appSettings.Secret);
        }

        [HttpGet("{strategy}")]
        public async Task<IActionResult> Get([FromRoute] Strategy strategy, [FromQuery] Coin coin)
        {
            _logger.LogInformation($"Evaluating the strategy {strategy}");

            var productId = "BTC-EUR";

            if (strategy == Strategy.MA5)
            {
                var candles = await _priceRepository.GetDailyRates(coin, 5);
                var currentPrice = await _priceRepository.GetCurrentPrice(productId);

                var strategyExecutor = new MovingAverageStrategy(candles, currentPrice);
                var result = strategyExecutor.Execute();

                _logger.LogInformation($"{candles.Count} Days Moving average is {result.ThresholdPrice}");
                _logger.LogInformation($"The current {productId} price is {currentPrice.price}");

                return Ok(result);
            }

            return BadRequest($"The {strategy} strategy is not implemented");

        }


    }
}
