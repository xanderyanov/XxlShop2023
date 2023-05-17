namespace XxlStore
{
    public class CSVFile
    {
        public class CSVHeaderItem
        {
            public string NameInFile;
            public string NameWeWant;
            public int Index;
        }

        Dictionary<string, CSVHeaderItem> ByName = new Dictionary<string, CSVHeaderItem>();

        char Separator;
        char Quotator;
        IEnumerator<string> LinePtr;
        List<string> CurrentRow;

        public CSVFile(IEnumerable<string> lines, char separator, char quotator, string namesInFile = null, string desiredNames = null)
        {
            Separator = separator;
            Quotator = quotator;

            string firstLine = lines.First();
            LinePtr = lines.GetEnumerator();  //Возвращает перечислитель, который осуществляет итерацию по коллекции.


            var F = StrUtils.Split(firstLine, Separator, Quotator);
            var R = StrUtils.Split(namesInFile ?? firstLine, Separator, Quotator);
            var N = StrUtils.Split(desiredNames ?? namesInFile ?? firstLine, Separator, Quotator);

            if (N.Count != R.Count) throw new Exception("namesInFile must have same quantity of items as desiredNames");

            for (int i = 0; i < N.Count; i++) {
                string n = N[i];
                string r = R[i];
                var I = new CSVHeaderItem() { NameWeWant = n, NameInFile = r };
                I.Index = F.IndexOf(r);
                ByName.Add(n, I);
            }
        }

        private string ValueByName(List<string> values, string name)
        {
            if (values == null) return null;
            if (string.IsNullOrEmpty(name)) return null;
            if (!ByName.TryGetValue(name, out var HI)) return null;

            int i = HI.Index;
            if (i < 0 || i >= values.Count) return null;

            return values[i];
        }

        public string this[string index]
        {
            get
            {
                return ValueByName(CurrentRow, index);
            }
        }

        public void Rewind()
        {
            LinePtr.Reset();
            LinePtr.MoveNext();
            CurrentRow = null;
        }

        public bool Next()
        {
            if (LinePtr.MoveNext()) {
                CurrentRow = StrUtils.Split(LinePtr.Current, Separator, Quotator);
                return true;
            } else {
                CurrentRow = null;
                return false;
            }
        }

        //public static void UsageSample()
        //{
        //    string[] lines = System.IO.File.ReadAllLines("Myfile.csv", System.Text.Encoding.UTF8);
        //    CSVFile csv = new(lines, ';', '"',
        //        "Код;Наименование;Артикул",
        //        "Code;Name;Article");
        //    csv.Rewind();
        //    while (csv.Next())
        //    {
        //        Console.WriteLine($"Код: {csv["Code"]}, Наименование: {csv["Name"]}, Артикул: {csv["Article"]}");
        //    }
        //}
    }
}
