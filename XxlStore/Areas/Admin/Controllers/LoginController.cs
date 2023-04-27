using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XxlStore.Infrastructure;
using XxlStore.Models;
using XxlStore.Models.ViewModels;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace XxlStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : XxlController
    {
        Domain domain = Data.MainDomain;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(LoginViewModel user)
        {
            TUser existUser = domain.ExistingUsers.SingleOrDefault(x => x.Name.ToLower() == user.Name.ToLower() && x.Password == HashPasswordHelper.HashPassword(user.Password));

            if (existUser == null || !existUser.IsActive) { return RedirectToAction("Index"); }

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Name.ToLower()),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, existUser.Role.Name)
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("AccessConfirmed");
        }

        [Authorize]
        public IActionResult AccessConfirmed()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }
    }
}


