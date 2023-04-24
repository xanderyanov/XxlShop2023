﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using XxlStore.Areas.Site.Controllers;
using XxlStore.Areas.Site.Models;
using XxlStore.Areas.Site.Infrastructure;
using XxlStore.Areas.Admin.Models.ViewModels;

namespace XxlStore.Areas.Admin.Controllers
{
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
            User existUser = domain.ExistingUsers.SingleOrDefault(x => x.Name.ToLower() == user.Name.ToLower() && x.Password == HashPasswordHelper.HashPassword(user.Password));

            if (existUser == null) { return RedirectToAction("Index"); }

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

            return View("Index");
        }
    }
}






//app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
//{
//    // получаем из формы email и пароль
//    var form = context.Request.Form;
//    // если email и/или пароль не установлены, посылаем статусный код ошибки 400
//    if (!form.ContainsKey("Name") || !form.ContainsKey("Password"))
//        return Results.BadRequest("Email и/или пароль не установлены");

//    string name = form["Name"];
//    string password = form["Password"];

//    // находим пользователя 
//    User person = Data.MainDomain.ExistingUsers.FirstOrDefault(p => p.Name == name && p.Password == HashPasswordHelper.HashPassword(password));
//    // если пользователь не найден, отправляем статусный код 401
//    if (person is null) return Results.Unauthorized();

//    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Name) };
//    // создаем объект ClaimsIdentity
//    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
//    // установка аутентификационных куки
//    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
//    return Results.Redirect(returnUrl ?? "/");
//});

//app.MapGet("/logout", async (HttpContext context) =>
//{
//    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//    return Results.Redirect("/login");
//});
