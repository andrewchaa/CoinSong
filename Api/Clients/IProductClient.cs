using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GdaxApi.Models;

namespace GdaxApi.Clients
{
    public interface IProductClient
    {
        Task<ApiResponse<IEnumerable<Product>>> GetProductsAsync();
        Task<ApiResponse<ProductPrice>> GetProductTickerAsync(string productId);
        Task<ApiResponse<OrderBook>> GetOrderBookAsync(string productId, int level = 1);
        Task<List<Candle>> GetHistoricRatesAsync(string productId, DateTimeOffset start, DateTimeOffset end, TimeSpan granularity);
    }
}