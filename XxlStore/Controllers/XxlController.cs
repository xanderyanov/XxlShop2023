using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using XxlStore.Models;

namespace XxlStore
{
    public class BaseBucket
    {
        internal string SelectedCategory;

        public string Title { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

        public TUser User { get; set; }
        public string UserName { get; set; }

        public bool OnlyCatalog { get; set; }

    }

    public class XxlController : Controller
    {
        public BaseBucket Bucket;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Bucket = new BaseBucket();
            Bucket.UserName = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            Bucket.User = Data.MainDomain.ExistingUsers.FirstOrDefault(x => x.Name == Bucket.UserName);
            Bucket.OnlyCatalog = false;
            ViewData["Bucket"] = Bucket;

            base.OnActionExecuting(context);
        }

        public class ViewSettingsClass
        {
            public bool NewOnly { get; set; } = false;
            public bool SaleLeaderOnly { get; set; } = false;
            public string InexpensivePrice { get; set; }

            public Dictionary<string, List<string>> CheckedFilters { get; set; } = new();

        }
    }
}
