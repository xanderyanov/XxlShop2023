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

//app.MapControllerRoute("pagination",
// "Products/Page{productPage}",
// new { Controller = "Home", action = "Index" });
//app.MapDefaultControllerRoute();


//app.MapControllerRoute("catpage",
// "{category}/Page{productPage:int}",
// new { Controller = "Home", action = "Index" });
//app.MapControllerRoute("page", "Page{productPage:int}",
// new { Controller = "Home", action = "Index", productPage = 1 });
//app.MapControllerRoute("category", "{category}",
// new { Controller = "Home", action = "Index", productPage = 1 });
//app.MapControllerRoute("pagination",
// "Products/Page{productPage}",
// new { Controller = "Home", action = "Index", productPage = 1 });
//app.MapDefaultControllerRoute();

//app.UseEndpoints(endpoints =>
//{

//    //endpoints.MapControllerRoute("Product",
//    //   "Product/{id?}",
//    //   new { controller = "Product", action = "Index" });

//    //endpoints.MapControllerRoute("Admin",
//    //       "Admin/{action}/{id?}",
//    //       new { controller = "Admin", action = "Index" });

//    endpoints.MapControllerRoute("Brand",
//            "Brand/{id?}",
//            new { controller = "Home", action = "Brand" });

//    endpoints.MapControllerRoute("Home",
//       "{controller}/{action}/{id?}",
//       new { controller = "Home", action = "Index" });

//    endpoints.MapDefaultControllerRoute();
//});

app.UseEndpoints(endpoints => {

    //endpoints.MapControllerRoute("route1",
    //   "blog/{action}",
    //   new { controller = "Blog", action = "Index" });

    //endpoints.MapControllerRoute("catpage",
    //    "{category}/Page{productPage:int}",
    //    new { Controller = "Home", action = "Index" });
    //endpoints.MapControllerRoute("page", "Page{productPage:int}",
    //    new { Controller = "Home", action = "Index", productPage = 1 });
    //endpoints.MapControllerRoute("category", "{category}",
    //    new { Controller = "Home", action = "Index", productPage = 1 });
    //endpoints.MapControllerRoute("pagination",
    //    "Products/Page{productPage}",
    //    new { Controller = "Home", action = "Index", productPage = 1 });



    //endpoints.MapControllerRoute("route1",
    //   "Product/{id?}",
    //   new { controller = "Product", action = "Product" });

    endpoints.MapControllerRoute("Brand",
        "Brand/{id?}",
        new { controller = "Catalog", action = "Brand" });    
    
    endpoints.MapControllerRoute("Catalog",
        "Catalog/{id?}",
        new { controller = "Catalog", action = "Index" });

    endpoints.MapControllerRoute("Default",
       "{controller}/{action}/{id?}",
       new { controller = "Home", action = "Index" });

    endpoints.MapDefaultControllerRoute();
});

app.Run();
