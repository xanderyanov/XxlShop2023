namespace XxlStore
{
    public class Settings
    {
        public static List<UrlPreset> SiteUrls = new() {
            new UrlPreset() { Url = "https://snesar.ru",           Version = 32, SpiesEnabled = true },
            new UrlPreset() { Url = "http://localhost:5000",        Version = 76, ImageSource = "https://snesar.ru" },
        };

        public static List<UrlPreset> AdminUrls = new() {
            new UrlPreset() { Url = "https://admon.snesar.ru",    Version = 32, SpiesEnabled = true },
            new UrlPreset() { Url = "http://admon.localhost:5000",     Version = 76, ImageSource = "https://admon.snesar.ru" },
        };


        public static readonly HostNameConstraint SiteHostNameConstraint = new(SiteUrls);
        public static readonly HostNameConstraint AdminHostNameConstraint = new(AdminUrls);
    }
}
