using Microsoft.AspNetCore.Mvc;

namespace XxlStore.Controllers
{
    public class BlogController : BaseController
    {
        public IActionResult Index()
        {
            Domain domain = Data.MainDomain;

            var posts = domain.ExistingPosts;

            return View("Index", posts);
        }
    }
}
