using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace XxlStore.Controllers
{
    public class ProductController : BaseController
    {
        public IActionResult Index(string id)
        {
            ObjectId Id = default;
            try {
                Id = new ObjectId(id);
            }
            catch {
                return NotFound();
            }

            Product product = Data.ExistingTovars.Find(x => x.Id == Id);

            return View("Product", product);
        }
    }
}
