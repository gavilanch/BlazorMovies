using BlazorMovies.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorMovies.Client.Repository
{
    interface IAccountsRepository
    {
        Task<UserToken> Login(UserInfo userInfo);
        Task<UserToken> Register(UserInfo userInfo);
    }
}
