using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net.Sockets;
using XxlStore.Models;

namespace XxlStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private JsonResult setOrChange(string id, string qty, bool? isInCart, Action<string, TOrder, OrderLine> Act)
        {
            InitShopData(OrderLoad.PickCurrent);

            if (Bucket.Order == null) Bucket.Order = CreateOrder(false);
            var order = Bucket.Order;

            if (!Utils.IsValidObjectId(id)) return new JsonResult(new { }); // null;
            TWareItem I = domain.WareItems.ById(new ObjectId(id));
            if (I == null) return new JsonResult(new { }); //null; // no such ware!

            OrderLine L = order.FindOrderLine(I);
            if (L == null) {
                L = new OrderLine(I, I.MainPack, 0);
                order.Lines.Add(L);
            }
            //L.UpdatePriceAndDiscount(Bucket);

            var Item = L.ItemRef.Value;
            if (!Item.PublishedExt || Item.FlagSoldOut || Item.mPrice <= 0) {
                L.PackQuantity = 0.0;
            } else {
                Act(qty, order, L);
            }

            order.CalcRequired();
            order.CalcTotals();
            order.Save(CRMSync.NotNeeded);

            bool IsInCart = isInCart ?? false;
            return new JsonResult(
            new OrderJS(order, IsInCart,
                    IsInCart ? RenderPartialViewToString(null, "CartTable", new CartTableModel(Bucket, IsInCart, true, MLSUse), VBSetter, Url.ActionContext, HttpContext) : null,
                Bucket));
        }

        [HttpPost]
        public JsonResult set(string id, string qty, bool? isInCart = null)
        {
            return setOrChange(id, qty, isInCart, setQty);
        }

        public IActionResult Create()
        {
            //if()

            //Cart cart = new Cart();
            //model.CreatedDate = DateTime.Now;

            return View("Edit");
        }
    }
}
