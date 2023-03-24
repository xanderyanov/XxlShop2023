using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace XxlStore
{
    public class BaseBucket
    {
        internal string SelectedCategory;

        public string Title { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }

    }

    public class BaseController : Controller
    {
        public BaseBucket Bucket = new BaseBucket();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
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
