using Microsoft.AspNetCore.Mvc;
using XxlStore.Models;

namespace XxlStore.Components
{
    //public class NavigationMenuViewComponent : ViewComponent
    //{
    //    public Task<string> InvokeAsync()
    //    {
    //        return Task.FromResult("Hello from the Nav View Component");
    //    }
    //}

    public class NavigationMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["id"];
            return View(Data.Categories);
        }
    }
}
