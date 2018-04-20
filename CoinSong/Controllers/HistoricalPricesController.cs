using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Domain.Models;
using CoinSong.Domain.Repositories;
using GdaxApi.Models;
using Microsoft.AspNetCore.Mvc;
using Sulmo;

namespace CoinSong.Controllers
{
    [Route("api/[controller]")]
    public class HistoricalPricesController : Controller
    {
        private readonly ICoinRepository _coinRepository;
        private readonly IPriceRepository _priceRepository;

        public HistoricalPricesController(ICoinRepository coinRepository,
            IPriceRepository priceRepository)
        {
            _coinRepository = coinRepository;
            _priceRepository = priceRepository;
        }
        
        [HttpGet("{coin}")]
        public async Task<IList<Candle>> Get([FromRoute] Coin coin, [FromQuery]int days)
        {
            return await _priceRepository.GetDailyRates(coin, days);
        }

        // POST api/values
        [HttpPost]
        public async Task Post()
        {
            var prices = await _coinRepository.GetPricesAsync();
            foreach (var price in prices)
            {
                await _priceRepository.SaveAsync(price);
            }
        }
    }
}