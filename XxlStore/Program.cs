using XxlStore;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using XxlStore.Areas.Admin.Controllers;
using XxlStore.Areas.Site.Controllers;
using XxlStore.Models;

var Prov = CodePagesEncodingProvider.Instance;
Encoding.RegisterProvider(Prov);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

Configure(app);

//app.UseAuthentication();   // добавление middleware аутентификации 

//app.UseStaticFiles();

//app.UseRouting();

//app.UseMiddleware<XxlStore.Middleware.AuthCorrectionMiddleware>();

//app.UseAuthorization();   // добавление middleware авторизации 

static void Configure(IApplicationBuilder app)
{
    app.UseStaticFiles();
    app.UseSession();


    app.MapWhen(
    context => Settings.AdminHostNameConstraint.Match(context),
    branch =>
    {
        branch.UseAuthentication();   // добавление middleware аутентификации 

        branch.UseRouting();

        branch.UseMiddleware<XxlStore.Middleware.AuthCorrectionMiddleware>();

        branch.UseAuthorization();   // добавление middleware авторизации 

        branch.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "Update/{action}/{id?}",
                defaults: new { area = "Admin", controller = nameof(UpdateController)[..^10], action = "Index" }
            ).WithDisplayName("Update");

            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller}/{action}/{id?}",
                defaults: new { area = "Admin", controller = nameof(AdminController)[..^10], action = "Index" }
            ).WithDisplayName("AdminDefault");

        });
    }
);

    app.MapWhen(
        context => Settings.SiteHostNameConstraint.Match(context),
        branch =>
        {

            branch.UseRouting();

            branch.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Product",
                    pattern: "Product/{id?}",
                    defaults: new { area = "Site", controller = nameof(ProductController)[..^10], action = "Index" }
                ).WithDisplayName("SiteProduct");

                endpoints.MapControllerRoute(
                    name: "Catalog",
                    pattern: "Catalog/{id?}",
                    defaults: new { area = "Site", controller = nameof(CatalogController)[..^10], action = "Index" }
                ).WithDisplayName("SiteCatalog");

                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller}/{action}/{id?}",
                    defaults: new { area = "Site", controller = nameof(HomeController)[..^10], action = "Index" }
                ).WithDisplayName("SiteDefault");

            });
        }
    );

    //app.UseEndpoints(endpoints => {

    //    endpoints.MapControllerRoute("Product",
    //       "Product/{id?}",
    //       new { controller = "Product", action = "Index" });

    //    endpoints.MapControllerRoute("Catalog",
    //        "Catalog/{id?}",
    //        new { controller = "Catalog", action = "Index" });

    //    endpoints.MapControllerRoute("Default",
    //       "{controller}/{action}/{id?}",
    //       new { controller = "Home", action = "Index" });

    //    endpoints.MapDefaultControllerRoute();
    //});

}

app.MapRazorPages();

app.Run();
