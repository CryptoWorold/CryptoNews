﻿using CryptoWorld.News.Core.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CryptoWorld.News.Core.Contracts
{
    public interface IAccountService
    {
        Task<LoginResponseModel> LoginAsync(LoginRequestModel model);
        Task<IdentityResult> RegisterAsync(RegisterRequestModel model);
    }
}