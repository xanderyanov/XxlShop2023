using Newtonsoft.Json;

namespace XxlStore
{
    public class DBConf
    {
        public string ConnectionString { get; set; }
        public string DBName { get; set; }

        public DBConf()
        {
        }

        public DBConf(IConfiguration Config)
        {
            ConnectionString = Config.GetValue<string>("ConnectionString");
            DBName = Config.GetValue<string>("DBName");
        }

        public DBConf(string DBConfig)
        {
            var tempconf = JsonConvert.DeserializeObject<DBConf>(DBConfig);
            ConnectionString = tempconf.ConnectionString;
            DBName = tempconf.DBName;
        }
    }
}
