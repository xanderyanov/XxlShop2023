using Microsoft.AspNetCore.Mvc;
using XxlStore;

namespace XxlStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Product> Products = Data.ExistingTovars;

            IEnumerable<Product> filteredProducts = Products.Where(x => x.FlagSaleLeader);

            return View(filteredProducts);
        }
    }
}
