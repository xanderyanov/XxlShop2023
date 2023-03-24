using Amazon.Runtime.Internal.Endpoints.StandardLibrary;

namespace XxlStore.Models
{
    public class MenuItem
    {
        public MenuItem Parent;

        public string Url;
        public MenuItem(string url)
        {
            Url = url;
        }

        public bool IsSelected;

        public List<MenuItem> Children = new();

        
    }
}
