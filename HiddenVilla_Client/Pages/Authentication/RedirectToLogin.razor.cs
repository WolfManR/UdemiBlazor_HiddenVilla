using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }

        private bool NotAuthorized { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;
            if(authState?.User?.Identity is null || !authState.User.Identity.IsAuthenticated)
            {
                var returnUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    NavigationManager.NavigateTo("login", true);
                }
                else
                {
                    NavigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
                }
            }
            else
            {
                NotAuthorized = true;
            }
        }
    }
}
