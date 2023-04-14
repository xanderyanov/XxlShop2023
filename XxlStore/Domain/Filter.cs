namespace XxlStore
{

    public class Filter
    {
        public static List<string> AllGenders { get; private set; }
        
        public static List<string> AllCaseForm { get; private set; }
        public static List<string> AllCaseMaterial { get; private set; }
        
        public static List<string> AllGlass { get; private set; }


        public static void CollectGlobalFilterValues()
        {
            Domain domain = Data.MainDomain;
            AllGenders = domain.ExistingTovars.Select(x => x.Gender).ToHashSet().ToList();
            AllCaseForm = domain.ExistingTovars.Select(x => x.CaseForm).ToHashSet().ToList();
            AllCaseMaterial = domain.ExistingTovars.Select(x => x.CaseMaterial).ToHashSet().ToList();
            AllGlass = domain.ExistingTovars.Select(x => x.Glass).ToHashSet().ToList();
        }


        public static void CollectPageFilterValues(IEnumerable<Product> Products)
        {
            AllGenders = Products.Select(x => x.Gender).ToHashSet().ToList();
            AllCaseForm = Products.Select(x => x.CaseForm).ToHashSet().ToList();
            AllCaseMaterial = Products.Select(x => x.CaseMaterial).ToHashSet().ToList();
            AllGlass = Products.Select(x => x.Glass).ToHashSet().ToList();
        }        


        public static void Clear()
        {
            AllGenders = null;
            AllCaseForm = null;
            AllCaseMaterial = null;
            AllGlass = null;
        }


    }




}
