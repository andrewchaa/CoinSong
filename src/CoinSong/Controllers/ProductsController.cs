using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Api.Infrastructure;
using GdaxApi.Authentication;
using GdaxApi.Clients;
using GdaxApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoinSong.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<CoinsController> _logger;
        private readonly GdaxOptions _gdaxOptions;
        private readonly RequestAuthenticator _authenticator;

        public ProductsController(ILogger<CoinsController> logger, IOptions<GdaxOptions> options)
        {
            _logger = logger;
            _gdaxOptions = options.Value;
            _authenticator = new RequestAuthenticator(_gdaxOptions.ApiKey, _gdaxOptions.Passphrase, _gdaxOptions.Secret);
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Candle>> Get(string id)
        {
            _logger.LogInformation($"Retrieving historic rates of {id.ToUpper()}-EUR");

            var client = new ProductClient("https://api.gdax.com", _authenticator);
            var response = await client.GetHistoricRatesAsync($"{id.ToUpper()}-EUR", DateTimeOffset.UtcNow.AddDays(-7),
                DateTimeOffset.UtcNow, TimeSpan.FromDays(1));

            return response;

        }


    }
}
