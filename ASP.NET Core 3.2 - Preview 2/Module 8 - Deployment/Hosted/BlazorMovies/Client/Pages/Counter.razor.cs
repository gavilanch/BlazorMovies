using BlazorMovies.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlazorMovies.Client.Shared.MainLayout;

namespace BlazorMovies.Client.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;

        [CascadingParameter] private Task<AuthenticationState> AuthenticationState { get; set; }

        public async Task IncrementCount()
        {
            var authState = await AuthenticationState;
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                currentCount++;
            }
            else
            {
                currentCount--;
            }
        }
    }
}
