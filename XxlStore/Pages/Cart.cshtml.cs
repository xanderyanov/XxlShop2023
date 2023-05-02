using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XxlStore.Infrastructure;
using XxlStore.Models;

namespace XxlStore.Pages
{
    public class CartModel : PageModel
    {

        IEnumerable<Product> productSource = Data.MainDomain.ExistingTovars;

        public CartModel(Cart cartService)
        {
            Cart = cartService;
        }
        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(string idAsString, string returnUrl)
        {
            Product? product = productSource.FirstOrDefault(p => p.IdAsString == idAsString);
            if (product != null) {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(string id, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
            cl.Product.IdAsString == id).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
