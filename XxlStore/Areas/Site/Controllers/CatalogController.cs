using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text;
using XxlStore.Models.ViewModels;
using Newtonsoft.Json;
using System.Reflection;
using Amazon.Runtime.Internal;
using XxlStore.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace XxlStore.Areas.Site.Controllers
{
    [Area("Site")]
    public class CatalogController : XxlController
    {
        public int PageSize = 16;

        public IActionResult Index(string id, int productPage = 1)
        {
            Domain domain = Data.MainDomain;

            var products = domain.ExistingTovars;

            var viewSettings = new ViewSettingsClass();
            ViewBag.ViewSettings = viewSettings;

            Bucket.SelectedCategory = id;
            Bucket.Title = $"Часы {id} в магазине Мир Часов XXL";

            IEnumerable<Product> Products = domain.ExistingTovars;

            IEnumerable<Product> productSource = domain.ExistingTovars;

            /// <summary>
            /// Request.Query - содержит пары ключ-значениЯ, которые он делает из строки параметров ключ-значениЕ, ключ-значениЕ - при совпадении ключей.
            /// ?f_Case=Прямоуг&f_Case=Овал&f_Gender=Male&f_Gender=Uni
            /// Key: f_Case
            /// Value: { "Прямоуг", "Овал" }
            /// Key: f_Gender
            /// Value: { "Male", "Uni" }
            /// </summary>

            if (Request.Query != null)
            {
                foreach (var pair in Request.Query)
                {
                    string filterKey = pair.Key;
                    if (filterKey.StartsWith("f_"))
                    {
                        string propName = filterKey[2..];
                        var values = pair.Value;

                        PropertyInfo PI = typeof(Product).GetProperty(propName); //проверяем, есть ли в Product свойство с именем propName, которое мы получили. И если есть, то код ниже выполняется.
                        if (PI != null)
                        {
                            List<string> decodedValues = values.Select(x => Base64Fix.Obratno(x)).ToList(); //получаем список свойств по-русски, декодировав наш полученный values
                            viewSettings.CheckedFilters.Add(propName, decodedValues); // подготовка к следующему заапросу с сохранением информации в viewSettings
                            List<Product> thisStepProds = productSource.Where(x => decodedValues.Contains(PI.GetValue(x) as string)).ToList(); //фильтруем 
                            productSource = thisStepProds;
                        }
                    }
                }
            }

            Products = productSource
                .Where(p => Bucket.SelectedCategory == null || p.BrandName == Bucket.SelectedCategory)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize);

            var ProductsForFiltersElements = productSource
                .Where(p => Bucket.SelectedCategory == null || p.BrandName == Bucket.SelectedCategory);

            //Console.WriteLine("productSource.Count - " + ProductsForFiltersElements.Count());

            Filter.CollectPageFilterValues(ProductsForFiltersElements);

            return View("Catalog", new ProductsListViewModel
            {
                Products = Products,
                ViewSettings = viewSettings,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = Bucket.SelectedCategory == null ? productSource.Count() : productSource.Where(e => e.BrandName == Bucket.SelectedCategory).Count()
                },
                CurrentCategory = Bucket.SelectedCategory
            });
        }
    }
}
