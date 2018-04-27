using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinSong.Api.Controllers;
using CoinSong.Api.Domain.Models;
using CoinSong.Api.Domain.Repositories;
using Moq;
using Sulmo;
using Xunit;

namespace CoinSong.Tests
{
    public class PricesControllerTests
    {
        private readonly Mock<ICoinRepository> _coinRepository;
        private readonly Mock<IPriceRepository> _priceRepository;
        private readonly RatesController _controller;

        public PricesControllerTests()
        {
            _coinRepository = new Mock<ICoinRepository>();
            _priceRepository = new Mock<IPriceRepository>();
            _controller = new RatesController(_coinRepository.Object, _priceRepository.Object);
            
        }
        
        [Fact]
        public async Task Should_return_prices_for_bth_eth_and_lit_coins()
        {
            // arrange

            // act
            await _controller.Get();
            
            // assert
            _coinRepository.Verify(r => r.GetRates());
        }

        [Fact]
        public async Task Should_return_price_for_given_coin()
        {
            // arrange
            var coin = "BTC";
            
            // act
            await _controller.Get(coin);
            
            // assert
            _coinRepository.Verify(r => r.GetPriceAsync(coin.To<Coin>()));
        }

        [Fact]
        public async Task Should_store_prices_into_database()
        {
            // arrange
            _coinRepository.Setup(r => r.GetRates()).ReturnsAsync(new List<SnapshotPrice>
            {
                new SnapshotPrice(Coin.BTC, "100", Currency.EUR, "100", "100", "0.1", DateTime.UtcNow),
                new SnapshotPrice(Coin.ETH, "100", Currency.EUR, "100", "100", "0.1", DateTime.UtcNow),
                new SnapshotPrice(Coin.LTC, "100", Currency.EUR, "100", "100", "0.1", DateTime.UtcNow)
            });
            
            // act
            await _controller.Post();
            
            // assert
            _coinRepository.Verify(r => r.GetRates());
            _priceRepository.Verify(r => r.SaveAsync(Moq.It.IsAny<SnapshotPrice>()), Times.Exactly(3));
        }
    }
}