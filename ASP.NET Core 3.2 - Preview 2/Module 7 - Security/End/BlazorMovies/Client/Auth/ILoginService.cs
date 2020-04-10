using BlazorMovies.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Auth
{
    public interface ILoginService
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task TryRenewToken();
    }
}
