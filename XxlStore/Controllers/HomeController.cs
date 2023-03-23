using Microsoft.AspNetCore.Mvc;
using XxlStore;

namespace XxlStore.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 4;
        public IActionResult Index(int productPage = 1)
        {
            IEnumerable<Product> Products = Data.ExistingTovars;

            IEnumerable<Product> filteredProducts = Products.Where(x => x.FlagSaleLeader);

                return View(Products
                             .OrderBy(p => p.Id) 
                             .Skip((productPage - 1) * PageSize)
                             .Take(PageSize)
                            );

        }
    }
}
