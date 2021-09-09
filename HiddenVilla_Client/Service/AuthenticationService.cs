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

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage)
        {
            this.client = client;
            this.localStorage = localStorage;
        }

        public Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegistration)
        {
            throw new NotImplementedException();
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
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);

                return new AuthenticationResponseDTO { IsAuthSuccessful = true };
            }

            return result;
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
