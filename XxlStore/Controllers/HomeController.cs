using Microsoft.AspNetCore.Mvc;
using XxlStore;
using XxlStore.Models.ViewModels;

namespace XxlStore.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 16;
        public IActionResult Index(int productPage = 1)
        {
            IEnumerable<Product> Products = Data.ExistingTovars;

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
                //CurrentCategory = id
            });

        }
    }
}
