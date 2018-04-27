using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GdaxApi.Models;

namespace GdaxApi.Clients
{
    public interface IAccountClient
    {
        Task<ApiResponse<IEnumerable<Account>>> ListAccountsAsync(Guid? accountId = null);
    }
}