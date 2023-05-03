using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using static XxlStore.XxlController;

namespace XxlStore.Views
{

    public interface IXxlViewHelper
    {
        void NavMenu();
        void FilterUl(List<string> filterName, string filterData, ViewSettingsClass viewSettings);
        void GroupenFilter(ViewSettingsClass viewSettings);
    }
    abstract public class XxlView<TModel> : RazorPage<TModel>
    {
        IXxlViewHelper viewStart;

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

        public IXxlViewHelper ViewStart
        {
            get
            {
                if (viewStart == null) {
                    viewStart = ViewData["ViewStart"] as IXxlViewHelper;
                }

                return viewStart;
            }
        }
    }
}
