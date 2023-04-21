using static XxlStore.XxlController;

namespace XxlStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
            = Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set; } = new();

        public string? CurrentCategory { get; set; }

        public ViewSettingsClass ViewSettings { get; set; }
    }
}
