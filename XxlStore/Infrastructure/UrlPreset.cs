namespace XxlStore
{
    public class UrlPresetSource
    {
        public string Url;
        public int Version;
        public string PublicUrl;
        public string BtlPublicUrl;
        public bool SpiesEnabled;
        public bool IsImageSource;
    }

    public class UrlPreset
    {
        public string Scheme { get; private set; }

        public string Host { get; private set; } // = Subdomain.Domain

        public string Port { get; private set; }

        public int Version;

        public bool SpiesEnabled;
        public string ImageSource;

        public string PublicUrl;
        public string BtlPublicUrl;

        public string Url
        {
            set
            {
                Uri U = new(value);
                Scheme = U.Scheme;
                Host = U.Host;
                Port = U.IsDefaultPort ? null : ":" + U.Port.ToString();
            }
        }

        //public UrlPreset(UrlPresetSource x)
        //{
        //    Version = x.Version;
        //    PublicUrl = x.PublicUrl;
        //    BtlPublicUrl = x.BtlPublicUrl;
        //    SpiesEnabled = x.SpiesEnabled;
        //    IsImageSource = x.IsImageSource;
        //}

        public string GetRoot()
        {
            return Scheme + "://" + Host + Port;
        }
    }
}
