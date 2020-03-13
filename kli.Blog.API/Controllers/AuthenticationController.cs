﻿using IdentityServer4.Extensions;
using kli.Blog.Core;
using kli.Blog.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace kli.Blog.API.Controllers
{
	public class AuthenticationController : BaseController
	{
		[HttpGet]
		public UserModel UserInfo()
		{
			return this.User.Identity.IsAuthenticated
				? new UserModel { Name = this.User.Identity.Name!, IsAuthenticated = true }
				: new UserModel();
		}

		[HttpPost]
		public async Task<ActionResult> SignIn([FromForm] string login, [FromForm] string password)
		{
			var claimsPrincipal = await this.Mediator.Send(new AuthenticateUser.Request { Login = login, Password = password });
			if (claimsPrincipal.IsAuthenticated())
			{
				var authProperties = new AuthenticationProperties
				{
					AllowRefresh = true, // Refreshing the authentication session should be allowed.
					ExpiresUtc = DateTimeOffset.UtcNow.AddHours(48), // The time at which the authentication ticket expires.
					IsPersistent = true,
					//IssuedUtc = <DateTimeOffset> // The time at which the authentication ticket was issued.
					//RedirectUri = <string> // The full path or absolute URI to be used as an http redirect response value.
				};

				await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
				return this.Ok();
			}
			
			return this.Unauthorized("Invalid sign in attempt.");
		}

		[HttpGet]
		public async Task<ActionResult> SignOut()
		{
			await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return this.LocalRedirect("/");
		}
	}
}