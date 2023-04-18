using Microsoft.AspNetCore.Mvc;

namespace XxlStore.Controllers
{
    public class AccountController : BaseController
    {

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            return View(model);
        }
    }
}
