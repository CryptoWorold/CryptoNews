﻿using CryptoWorld.News.Core.Contracts;
using CryptoWorld.News.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWorld.Application.Server.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly IAccountService accountService;

		public AccountController(IAccountService _accountService)
		{
			accountService = _accountService;
		}

		[HttpPost("register")]
		[AllowAnonymous]
		public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var result = await accountService.RegisterAsync(model);

				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return BadRequest(ModelState);
				}
				else
				{
					return Ok(new { Message = "The user was registered successfully!" });
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var token = await accountService.LoginAsync(model);

			return Ok(token);
		}
	}
}