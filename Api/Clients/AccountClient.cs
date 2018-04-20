using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GdaxApi.Authentication;
using GdaxApi.Models;

namespace GdaxApi.Clients
{
    public class AccountClient : GdaxClient, IAccountClient
    {
        public AccountClient(string baseUrl, RequestAuthenticator authenticator)
            : base(baseUrl, authenticator) {}

        public async Task<ApiResponse<IEnumerable<Account>>> ListAccountsAsync(Guid? accountId = null)
        {
            return await GetResponseAsync<IEnumerable<Account>>(
                new ApiRequest(HttpMethod.Get, $"/accounts/{accountId?.ToString().ToLower()}")
            );
        }
    }
}