using Microsoft.AspNetCore.Mvc;

namespace XxlStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
