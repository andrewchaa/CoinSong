using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GdaxApi.Models;

namespace GdaxApi.Clients
{
    public interface IOrderClient
    {
        Task<ApiResponse<IEnumerable<Order>>> GetOpenOrdersAsync();
        Task<ApiResponse<IEnumerable<Guid>>> CancelOpenOrdersAsync(string productId = null);
        Task<ApiResponse<Order>> PlaceOrderAsync(string side, string productId, decimal size, decimal price, string type, string cancelAfter = null, string timeInForce = null);
        Task<ApiResponse<Order>> PlaceOrderAsync(string side, string productId, decimal size, decimal price, string type, bool postOnly, string cancelAfter = null, string timeInForce = null);
    }
}