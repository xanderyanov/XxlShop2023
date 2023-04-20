using XxlStore;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System;
using XxlStore.Models;
using Microsoft.AspNetCore.Authorization;
using XxlStore.Infrastructure;

var Prov = CodePagesEncodingProvider.Instance;
Encoding.RegisterProvider(Prov);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

Data.InitData(builder.Configuration);

// аутентификация с помощью куки
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login/Accessdenied";
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();   // добавление middleware аутентификации 



//app.MapGet("/login", async (HttpContext context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    // html-форма для ввода логина/пароля
//    string loginForm = @"<!DOCTYPE html>
//    <html>
//    <head>
//        <meta charset='utf-8' />
//        <title>METANIT.COM</title>
//    </head>
//    <body>
//        <h2>Login Form</h2>
//        <form method='post'>
//            <p>
//                <label>Name</label><br />
//                <input name='Name' />
//            </p>
//            <p>
//                <label>Password</label><br />
//                <input type='password' name='Password' />
//            </p>
//            <input type='submit' value='Login' />
//        </form>
//    </body>
//    </html>";
//    await context.Response.WriteAsync(loginForm);
//});

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
//    return Results.Redirect("/Login/Index");
//});

//app.MapGet("/", () => "Hello World!");

//app.Map("/", [Authorize] () => $"Hello World!");


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();   // добавление middleware авторизации 

app.UseEndpoints(endpoints => {

    endpoints.MapControllerRoute("Product",
       "Product/{id?}",
       new { controller = "Product", action = "Index" });

    endpoints.MapControllerRoute("Catalog",
        "Catalog/{id?}",
        new { controller = "Catalog", action = "Index" });

    //endpoints.MapControllerRoute("BlogEdit",
    //   "Blog/{action}/{id?}",
    //   new { controller = "Blog", action = "Index" });
    
    //endpoints.MapControllerRoute("CreatePost",
    //   "Blog/CreatePost/{id?}",
    //   new { controller = "Blog", action = "CreatePost" });
    //endpoints.MapControllerRoute("DeletePost",
    //   "Blog/DeletePost/{id?}",
    //   new { controller = "Blog", action = "DeletePost" });
    //endpoints.MapControllerRoute("Blog",
    //   "Blog/{id?}",
    //   new { controller = "Blog", action = "Index" });

    endpoints.MapControllerRoute("Default",
       "{controller}/{action}/{id?}",
       new { controller = "Home", action = "Index" });

    endpoints.MapDefaultControllerRoute();
});

app.Run();
