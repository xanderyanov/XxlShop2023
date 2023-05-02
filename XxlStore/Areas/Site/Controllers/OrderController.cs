using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;
using XxlStore.Models;

namespace XxlStore.Areas.Site.Controllers
{
    [Area("Site")]
    public class OrderController : XxlController
    {
        Domain domain = Data.MainDomain;

        private Cart cart;

        public OrderController(Cart cartService)
        {
            cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (order.Id == default) {
                order.Id = ObjectId.GenerateNewId();
            }

            BsonDocument filter = new BsonDocument() {
                { "_id", order.Id }
            };


            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid) {
                order.Lines = cart.Lines.ToArray();

                order.CreatedDate = DateTime.Now;

                //добавляем заказ в заказы в базу
                Data.ordersCollection.ReplaceOne(filter, order, new ReplaceOptions()
                {
                    IsUpsert = true
                });

                //добавляем заказ в заказы в памяти
                domain.ExistingOrders.Add(order);


                //Когда будет редактирование заказов в админке
                //if (!domain.ExistingOrders.Any(x => x.Id == order.Id)) {
                //    domain.ExistingOrders.Add(order);
                //} else {
                //    var mOrders = domain.ExistingOrders;
                //    int index = mOrders.IndexOf(mOrders.Where(x => x.Id == order.Id).FirstOrDefault());
                //    mOrders[index] = order;
                //}

                cart.Clear();

                return RedirectToPage("/Completed", new { orderId = order.Id });
            } else {
                return View();
            }
        }
    }
}
