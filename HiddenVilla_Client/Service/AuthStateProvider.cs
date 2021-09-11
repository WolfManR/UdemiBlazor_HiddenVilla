using Blazored.LocalStorage;

using Common;

using HiddenVilla_Client.Helper;

using Microsoft.AspNetCore.Components.Authorization;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>(SD.Local_Token);

            if (token is null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserLoggedIn(string token)
		{
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
		}

        public void NotifyUserLogout()
		{
            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
