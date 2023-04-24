using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace XxlStore.Views
{

    public interface XxlViewHelper
    {
        void NavMenu();
    }
    abstract public class XxlView<TModel> : RazorPage<TModel>
    {
        XxlViewHelper viewStart;

        protected BaseBucket Bucket;

        protected void InitializePage()
        {
            Bucket = ViewContext.ViewData["Bucket"] as BaseBucket;
        }

        private IHtmlHelper<TModel> html0;
        [RazorInject]
        public IHtmlHelper<TModel> Html0
        {
            get { return html0; }
            set
            {
                html0 = value;
                InitializePage();
            }
        }

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
