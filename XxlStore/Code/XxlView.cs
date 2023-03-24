using Microsoft.AspNetCore.Mvc.Razor;

namespace XxlStore.Views
{

    public interface XxlViewHelper
    {
        void NavMenu();
    }
    abstract public class XxlView<TModel> : RazorPage<TModel>
    {
        XxlViewHelper viewStart;
        public XxlViewHelper ViewStart
        {
            get
            {
                if (viewStart == null) {
                    viewStart = ViewData["ViewStart"] as XxlViewHelper;
                }
                return viewStart;
            }
        }
    }
}
