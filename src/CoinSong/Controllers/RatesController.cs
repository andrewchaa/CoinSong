using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Api.Domain.Models;
using CoinSong.Api.Domain.Repositories;
using GdaxApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoinSong.Api.Controllers
{
    [Route("api/[controller]")]
    public class RatesController : Controller
    {
        private readonly ICoinRepository _coinRepository;
        private readonly IPriceRepository _priceRepository;

        public RatesController(ICoinRepository coinRepository,
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
        public async Task Post([FromBody] Coin coin)
        {
            var prices = await _coinRepository.GetDailyRates(coin, 180);
//            foreach (var price in prices)
//            {
//                await _priceRepository.SaveAsync(price);
//            }
        }
    }
}