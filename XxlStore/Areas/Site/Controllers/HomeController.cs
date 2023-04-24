using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text;
using XxlStore.Models.ViewModels;

namespace XxlStore.Areas.Site.Controllers
{
    [Area("Site")]
    public class HomeController : XxlController
    {
        public int PageSize = 16;

        //[Authorize]
        public IActionResult Index(int productPage = 1)
        {
            IEnumerable<Product> Products = Data.MainDomain.ExistingTovars;

            IEnumerable<Product> filteredProducts = Products.Where(x => x.FlagNew);

            return View(new ProductsListViewModel
            {
                Products = filteredProducts
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = filteredProducts.Count()
                }
            });

        }
    }
}
