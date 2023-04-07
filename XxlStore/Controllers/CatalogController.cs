using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text;
using XxlStore;
using XxlStore.Models.ViewModels;
using Newtonsoft.Json;
using System.Reflection;
using Amazon.Runtime.Internal;

namespace XxlStore.Controllers
{
    //public class CatalogController : BaseController
    //{
    //    public int PageSize = 16;

    //    public IActionResult Brand(string id, int productPage = 1, string viewSettingsStr = null)
    //    {
    //        var products = Data.ExistingTovars;

    //        ViewSettingsClass viewSettings = null;
    //        try {
    //            viewSettings = JsonConvert.DeserializeObject<ViewSettingsClass>(Encoding.UTF8.GetString(Convert.FromBase64String(viewSettingsStr)));
    //        }
    //        catch {
    //            viewSettings = new();
    //        }

    //        IEnumerable<Product> Products = Data.ExistingTovars;

    //        ViewBag.ViewSettings = viewSettings;

    //        Bucket.SelectedCategory = id;

    //        Products = Products
    //            .Where(p => id == null || p.BrandName == id);

    //        Filter.CollectPageFilterValues(Products);

    //        var Model = new ProductsListViewModel
    //        {
    //            Products = products
    //               .Where(p => id == null || p.BrandName == id)
    //               //.OrderBy(p => p.Id)
    //               .Skip((productPage - 1) * PageSize)
    //               .Take(PageSize),
    //            PagingInfo = new PagingInfo
    //            {
    //                CurrentPage = productPage,
    //                ItemsPerPage = PageSize,
    //                TotalItems = id == null ? products.Count() : products.Where(e => e.BrandName == id).Count()

    //            },
    //            CurrentCategory = id
    //        };

    //        return View("Index", Model);
    //    }
    //    public IActionResult Index(string? category, int productPage = 1)
    //    {
    //        IEnumerable<Product> Products = Data.ExistingTovars;

    //        IEnumerable<Product> filteredProducts = Products.Where(x => x.FlagNew);

    //        return View(new ProductsListViewModel
    //        {
    //            Products = filteredProducts
    //            .Where(p => category == null || p.BrandName == category)
    //            .Skip((productPage - 1) * PageSize)
    //            .Take(PageSize),
    //            PagingInfo = new PagingInfo
    //            {
    //                CurrentPage = productPage,
    //                ItemsPerPage = PageSize,
    //                TotalItems = filteredProducts.Count()
    //            },
    //            CurrentCategory = category
    //        });

    //    }
    //}

    public class CatalogController : BaseController
    {
        public int PageSize = 16;

        public IActionResult Brand(string id, int productPage = 1, string viewSettingsStr = null)
        {
            var products = Data.ExistingTovars;

            ViewSettingsClass viewSettings = null;
            try {
                viewSettings = JsonConvert.DeserializeObject<ViewSettingsClass>(Encoding.UTF8.GetString(Convert.FromBase64String(viewSettingsStr)));
            }
            catch {
                viewSettings = new();
            }

            ViewBag.ViewSettings = viewSettings;

            Bucket.SelectedCategory = id;
            Bucket.Title = $"Часы {id} в магазине Мир Часов XXL";

            IEnumerable<Product> Products = Data.ExistingTovars;

            Filter.CollectPageFilterValues(Products);

            //Products = Products.Where(p =>
            //    (!viewSettings.NewOnly || p.FlagNew) &&
            //    (!viewSettings.SaleLeaderOnly || p.FlagSaleLeader) &&
            //    (string.IsNullOrEmpty(viewSettings.InexpensivePrice) || p.DiscountPrice < Double.Parse(viewSettings.InexpensivePrice)) &&
            //    (p.Gender == "Мужские")
            //);

            Products = Products
                .Where(p => id == null || p.BrandName == id);

            Filter.CollectPageFilterValues(Products);

            Products = Products
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize);

            return View("Catalog", new ProductsListViewModel
            {
                Products = Products,
                ViewSettings = viewSettings,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = id == null ? products.Count() : products.Where(e => e.BrandName == id).Count()
                },
                CurrentCategory = id
            });
        }

        //public Dictionary<string, string> CheckedFilters = new();
        public IActionResult Index(string id, int productPage, string viewSettingsStr)
        {
            if (productPage == 0) productPage = 1;

            ViewSettingsClass viewSettings = null;
            try {
                viewSettings = JsonConvert.DeserializeObject<ViewSettingsClass>(Encoding.UTF8.GetString(Convert.FromBase64String(viewSettingsStr)));
            }
            catch {
                viewSettings = new();
            }

            /*************/
            ViewData["Booo"] = new[] { 10, 20, 30 };
            ViewBag.ViewBagData = new[] { 100, 200, 300 };
            /*************/

            ViewBag.ViewSettings = viewSettings;

            IEnumerable<Product> Products = Data.ExistingTovars;

            Filter.CollectPageFilterValues(Products);

            //ППРОЧИТАТЬ И ВСПОМНИТЬ!!!
            //Request.Query - содержит пары ключ-значениЯ, которые он делает из строки параметров ключ-значениЕ, ключ-значениЕ - при совпадении ключей.
            // ?f_Case=Прямоуг&f_Case=Овал&f_Gender=Male&f_Gender=Uni
            // Key: f_Case
            // Value: { "Прямоуг", "Овал" }
            // Key: f_Gender
            // Value: { "Male", "Uni" }

            //public string propertyName;
            //public string propertyName;

            IEnumerable<Product> productSource = Data.ExistingTovars;

            foreach (var pair in Request.Query) { //
                string filterKey = pair.Key;
                if (filterKey.StartsWith("f_")) {
                    string propName = filterKey[2..];
                    var values = pair.Value;

                    PropertyInfo PI = typeof(Product).GetProperty(propName); //проверяем, есть ли в Product свойство с именем propName, которое мы получили. И если есть, то код ниже выполняется.
                    if (PI != null) {
                        List<string> decodedValues = values.Select(x => Base64Fix.Obratno(x)).ToList(); //получаем список свойств по-русски, декодировав наш полученный values
                        viewSettings.CheckedFilters.Add(propName, decodedValues); // подготовка к следующему заапросу с сохранением информации в viewSettings
                        List<Product> thisStepProds = productSource.Where(x => decodedValues.Contains(PI.GetValue(x) as string)).ToList(); //фильтруем 
                        productSource = thisStepProds;
                    }
                }
            }


            //filter = Request.Query["f"];

            Products = productSource
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize);


            return View("Catalog", new ProductsListViewModel
            {
                Products = Products,
                ViewSettings = viewSettings,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = productSource.Count()
                },
                CurrentCategory = id
            });
        }
    }
}
