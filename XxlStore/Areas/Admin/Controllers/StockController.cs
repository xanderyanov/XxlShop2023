using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
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

        [Authorize(Roles = "xander, admin, user")]
        public IActionResult Update(string idAsString)
        {
            Product product = domain.ExistingTovars.SingleOrDefault(x => x.IdAsString == idAsString);
            
            return View("ProductEdit", product);
        }

        [HttpPost]
        [Authorize(Roles = "xander, admin")]
        public IActionResult CreateOrUpdateProduct(Product product)
        {

            if (product.Id == default) {
                product.Id = ObjectId.GenerateNewId();
            }
            BsonDocument filter = new BsonDocument() {
                {
                    "_id", product.Id
                }
            };

            var updateSettings = new BsonDocument("$set", new BsonDocument { { "Name", product.Name }, { "DiscountPrice", product.DiscountPrice } });
            Data.productsCollection.UpdateOne(filter, updateSettings);



            var mTovars = domain.ExistingTovars;
            int index = mTovars.IndexOf(mTovars.Where(x => x.Id == product.Id).FirstOrDefault());
            mTovars[index].DiscountPrice = product.DiscountPrice;


            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "xander, admin")]
        public IActionResult DeleteProduct(string Id)
        {
            if (ObjectId.TryParse(Id, out var productId)) {

                BsonDocument filter = new BsonDocument() {
                    {
                        "_id", productId
                    }
                };

                Data.productsCollection.DeleteOne(filter);

                Domain domain = Data.MainDomain;

                domain.ExistingTovars.RemoveAll(x => x.Id == productId);    
            }
            return RedirectToAction("Index");
        }


    }
}
