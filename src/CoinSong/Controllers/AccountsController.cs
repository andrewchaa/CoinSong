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
    public class AccountsController : Controller
    {
        private readonly ILogger<CoinsController> _logger;
        private readonly GdaxOptions _gdaxOptions;

        public AccountsController(ILogger<CoinsController> logger, IOptions<GdaxOptions> options)
        {
            _logger = logger;
            _gdaxOptions = options.Value;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            _logger.LogInformation($"Retrieving all accounts information");

            var authenticator = new RequestAuthenticator(_gdaxOptions.ApiKey, _gdaxOptions.Passphrase, _gdaxOptions.Secret);
            var client = new AccountClient(_gdaxOptions.BaseUri, authenticator);
            var response = await client.ListAccountsAsync();

            return response.Value;

        }


    }
}
