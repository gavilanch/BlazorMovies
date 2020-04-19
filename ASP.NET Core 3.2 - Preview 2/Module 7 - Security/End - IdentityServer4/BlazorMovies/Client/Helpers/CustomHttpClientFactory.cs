using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Helpers
{
    public class CustomHttpClientFactory
    {
        private readonly HttpClient httpClient;
        private readonly IAccessTokenProvider authenticationService;
        private readonly NavigationManager navigationManager;
        private readonly string authorizationHeader = "Authorization";

        public CustomHttpClientFactory(HttpClient httpClient,
            IAccessTokenProvider authenticationService,
            NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.authenticationService = authenticationService;
            this.navigationManager = navigationManager;
        }

        public HttpClient GetHttpClientWithoutToken()
        {
            if (httpClient.DefaultRequestHeaders.Contains(authorizationHeader))
            {
                httpClient.DefaultRequestHeaders.Remove(authorizationHeader);
            }

            return httpClient;
        }

        public async Task<HttpClient> GetHttpClientWithToken()
        {
            if (!httpClient.DefaultRequestHeaders.Contains(authorizationHeader))
            {

                var tokenResult = await authenticationService.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    httpClient.DefaultRequestHeaders.Add(authorizationHeader, $"Bearer {token.Value}");
                }
                else
                {
                    navigationManager.NavigateTo(tokenResult.RedirectUrl);
                }
            }

            return httpClient;
        }
    }
}
