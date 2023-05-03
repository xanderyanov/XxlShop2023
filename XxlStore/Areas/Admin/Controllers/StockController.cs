using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Data;

namespace XxlStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockController : XxlController
    {
        Domain domain = Data.MainDomain;
        
        [Authorize(Roles = "xander, admin")]
        public IActionResult Index()
        {
            var products = domain.ExistingTovars.ToList();

            List<string> brands = domain.Categories;
            
            AutoDictionary<string, List<Product>> ProductsByBrand = new();

            foreach (var brand in brands) {
                foreach (var product in products) {
                    if (product.BrandName == brand) {
                        ProductsByBrand[brand].Add(product);
                    }
                }
            }


            return View("Index", ProductsByBrand);
        }

        
    }
}
