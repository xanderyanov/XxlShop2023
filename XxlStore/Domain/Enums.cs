namespace XxlStore
{
    public class EnumBase
    {
        public object Key { get; set; }
        public string Value { get; set; }

        public EnumBase(object key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class EnumBaseList : List<EnumBase> // TODO: Move to Dictionary
    {
        public string GetByKey(object K)
        {
            for (int i = 0; i < Count; i++)
            {
                var x = this[i];
                if (x.Key.Equals(K)) return x.Value;
            }
            return null;
        }

    }
}
