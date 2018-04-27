using System.Linq;
using System.Threading.Tasks;
using CoinSong.Api.Domain.Models;
using CoinSong.Api.Infrastructure;
using CoinSong.Api.ViewModels;
using GdaxApi.Authentication;
using GdaxApi.Clients;
using GdaxApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoinSong.Api.Controllers
{
    [Route("api/[controller]")]
    public class CoinsController : Controller
    {
        private readonly ILogger<CoinsController> _logger;
        private readonly GdaxOptions _gdaxOptions;
        private readonly RequestAuthenticator _authenticator;

        public CoinsController(ILogger<CoinsController> logger, IOptions<GdaxOptions> options)
        {
            _logger = logger;
            _gdaxOptions = options.Value;
            _authenticator = new RequestAuthenticator(_gdaxOptions.ApiKey, _gdaxOptions.Passphrase, _gdaxOptions.Secret);
        }


        [HttpPost("{id}")]
        public async Task<Order> Post(string id, [FromBody]OrderRequestModel request)
        {
            _logger.LogInformation($"Placing a market buy order for {id.ToUpper()}");

            var client = new OrderClient(_gdaxOptions.BaseUri, _authenticator);
            var response = await client.PlaceMarketBuyOrderAsync($"{id.ToUpper()}-EUR", request.Funds);

            _logger.LogInformation($"The order id is {response.Value.id}");

            return response.Value;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<Order> Delete(string id)
        {
            _logger.LogInformation($"Getting the total balance of {id.ToUpper()}");
            var accountClient = new AccountClient(_gdaxOptions.BaseUri, _authenticator);
            var accountResponse = await accountClient.ListAccountsAsync();
            var account = accountResponse.Value.First(a => a.currency == Currency.BTC.ToString());

            _logger.LogInformation($"Placing a market sell order for {id.ToUpper()}");

            var client = new OrderClient(_gdaxOptions.BaseUri, _authenticator);
            var response = await client.PlaceMarketSellOrderAsync($"{id.ToUpper()}-EUR", account.balance);

            return response.Value;
        }
    }
}
