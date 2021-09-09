using HiddenVilla_Client.Service.IService;

using Microsoft.AspNetCore.Components;

using Models;

using System;
using System.Threading.Tasks;
using System.Web;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Login
    {
        private readonly AuthenticationDTO UserForAuthentication = new AuthenticationDTO();
        public bool IsProcessing { get; set; }
        public bool ShowAuthenticationErrors { get; set; }
        public string Errors { get; set; }

        public string ReturnUrl { get; set; }

        [Inject]
        public IAuthenticationService authenticationService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }


        private async Task LoginUser()
        {
            ShowAuthenticationErrors = false;
            IsProcessing = true;

            var result = await authenticationService.Login(UserForAuthentication);
            if (result.IsAuthSuccessful)
            {
                IsProcessing = false;

                var absoluteUri = new Uri(navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }

                navigationManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
        }
    }
}
