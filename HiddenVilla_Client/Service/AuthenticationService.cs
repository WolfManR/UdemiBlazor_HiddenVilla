using Blazored.LocalStorage;

using HiddenVilla_Client.Service.IService;

using Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly ILocalStorageService localStorage;
		private readonly AuthenticationStateProvider authenticationStateProvider;

		public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.client = client;
            this.localStorage = localStorage;
			this.authenticationStateProvider = authenticationStateProvider;
		}

        public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/account/signup", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RegistrationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegistrationResponseDTO { IsRegistrationSuccessful = true };
            }

            return result;
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/account/signin", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthenticationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDTO);

                ((AuthStateProvider)authenticationStateProvider).NotifyUserLoggedIn(result.Token);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return new AuthenticationResponseDTO { IsAuthSuccessful = true };
            }

            return result;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync(SD.Local_Token);
            await localStorage.RemoveItemAsync(SD.Local_UserDetails);
            ((AuthStateProvider)authenticationStateProvider).NotifyUserLogout();
            client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
