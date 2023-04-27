using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XxlStore.Infrastructure;
using XxlStore.Models;

namespace XxlStore.Pages
{
    public class CartModel : PageModel
    {
        Domain domain = Data.MainDomain;
        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(string code1C, string returnUrl)
        {
            Product? product = domain.ExistingTovars.FirstOrDefault(p => p.Code1C == code1C);
            if (product != null) {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
