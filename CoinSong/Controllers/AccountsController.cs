using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Infrastructure;
using CoinSong.ViewModels;
using GdaxApi.Authentication;
using GdaxApi.Clients;
using GdaxApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoinSong.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ILogger<CoinsController> _logger;
        private readonly AppSettings _appSettings;

        public AccountsController(ILogger<CoinsController> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _appSettings = options.Value;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            _logger.LogInformation($"Retrieving all accounts information");

            var authenticator = new RequestAuthenticator(_appSettings.ApiKey, _appSettings.Passphrase, _appSettings.Secret);
            var client = new AccountClient(_appSettings.BaseUri, authenticator);
            var response = await client.ListAccountsAsync();

            return response.Value;

        }


    }
}
