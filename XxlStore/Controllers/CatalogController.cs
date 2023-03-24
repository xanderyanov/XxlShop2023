using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text;
using XxlStore;
using XxlStore.Models.ViewModels;

namespace XxlStore.Controllers
{
    public class CatalogController : BaseController
    {
        public int PageSize = 16;

        public IActionResult Brand(string id, int productPage = 1)
        {
            var products = Data.ExistingTovars;

            Bucket.SelectedCategory = id;

            var Model = new ProductsListViewModel
            {
                Products = products
                   .Where(p => id == null || p.BrandName == id)
                   //.OrderBy(p => p.Id)
                   .Skip((productPage - 1) * PageSize)
                   .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = id == null ? products.Count() : products.Where(e => e.BrandName == id).Count()

                },
                CurrentCategory = id
            };

            return View("Index", Model);
        }
        public IActionResult Index(string? category, int productPage = 1)
        {
            IEnumerable<Product> Products = Data.ExistingTovars;

            IEnumerable<Product> filteredProducts = Products.Where(x => x.FlagNew);

            return View(new ProductsListViewModel
            {
                Products = filteredProducts
                .Where(p => category == null || p.BrandName == category)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = filteredProducts.Count()
                },
                CurrentCategory = category
            });

        }
    }
}
