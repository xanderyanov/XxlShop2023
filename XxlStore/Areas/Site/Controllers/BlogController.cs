using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using XxlStore.Areas.Site.Models;

namespace XxlStore.Areas.Site.Controllers
{
    [Area("Site")]
    public class BlogController : XxlController
    {

        [Authorize]
        public IActionResult Index()
        {
            Domain domain = Data.MainDomain;

            var posts = domain.ExistingPosts.OrderByDescending(x => x.CreatedDate).ToList();

            return View("Index", posts);
        }

        public IActionResult Post(string id)
        {
            ObjectId Id = default;
            try
            {
                Id = new ObjectId(id);
            }
            catch
            {
                return NotFound();
            }

            Post post = Data.MainDomain.ExistingPosts.Find(x => x.Id == Id);

            return View("Post", post);
        }
    }
}
