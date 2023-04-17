using XxlStore;
using System.Text;

var Prov = CodePagesEncodingProvider.Instance;
Encoding.RegisterProvider(Prov);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

Data.InitData(builder.Configuration);

//Data.ImportCSV();     //Обновление товаров

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints => {

    endpoints.MapControllerRoute("Product",
       "Product/{id?}",
       new { controller = "Product", action = "Index" });

    endpoints.MapControllerRoute("Catalog",
        "Catalog/{id?}",
        new { controller = "Catalog", action = "Index" });

    endpoints.MapControllerRoute("Blog",
       "Blog/{action}/{id?}",
       new { controller = "Blog", action = "Index" });

    endpoints.MapControllerRoute("Default",
       "{controller}/{action}/{id?}",
       new { controller = "Home", action = "Index" });

    endpoints.MapDefaultControllerRoute();
});

app.Run();
