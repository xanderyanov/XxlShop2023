using XxlStore;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

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

app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<XxlStore.Middleware.AuthCorrectionMiddleware>();

app.UseAuthorization();   // добавление middleware авторизации 

app.UseEndpoints(endpoints => {

    endpoints.MapControllerRoute("Product",
       "Product/{id?}",
       new { controller = "Product", action = "Index" });

    endpoints.MapControllerRoute("Catalog",
        "Catalog/{id?}",
        new { controller = "Catalog", action = "Index" });
        
    endpoints.MapControllerRoute("Default",
       "{controller}/{action}/{id?}",
       new { controller = "Home", action = "Index" });

    endpoints.MapDefaultControllerRoute();
});

app.Run();
