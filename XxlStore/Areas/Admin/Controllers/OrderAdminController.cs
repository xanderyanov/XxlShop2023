using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Data;

namespace XxlStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderAdminController : XxlController
    {
        Domain domain = Data.MainDomain;
        
        [Authorize(Roles = "xander, admin")]
        public IActionResult Index()
        {
            var orders = domain.ExistingOrders.ToList();

            return View("OrderList", orders);
        }

        [HttpPost]
        [Authorize(Roles = "xander, admin")]
        public IActionResult DeleteOrder(string Id)
        {
            //передавать на удаление надо Id того типа, который хранится в базе. Мы передаем string, а в базе ObjectId. По этому делаем проверку ObjectId.TryParse(Id, out var postId)

            if (ObjectId.TryParse(Id, out var orderId)) {

                BsonDocument filter = new BsonDocument() {
                    {
                        "_id", orderId
                    }
                };

                Data.ordersCollection.DeleteOne(filter);

                //Data.LoadObjects();      //метод удаления поста - переинициализация всех объектов из базы (в потенциале - затратно)

                Domain domain = Data.MainDomain;

                domain.ExistingOrders.RemoveAll(x => x.Id == orderId);    
            }
            return RedirectToAction("Index");
        }
    }
}
