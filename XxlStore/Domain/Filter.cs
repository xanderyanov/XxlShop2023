namespace XxlStore
{
    public class GroupenFilter
    { 
 
    }

    public class Filter
    {
        public static List<string> AllGenders { get; private set; }
        public static List<string> AllMechanismType { get; private set; }
        public static List<string> AllCaseForm { get; private set; }
        public static List<string> AllCaseMaterial { get; private set; }
        public static List<string> AllGlass { get; private set; }
        public static bool FlagNew { get; private set; }
        public static bool OnSkladHM { get; private set; }
        public static bool OnSkladGL { get; private set; }
        public static bool OnSkladLZ { get; private set; }
        public static bool OnSkladOR { get; private set; }
        public static bool OnSkladPZ { get; private set; }
        public static bool OnSkladTA { get; private set; }

        public static void CollectGlobalFilterValues()
        {
            Domain domain = Data.MainDomain;
            AllGenders = domain.ExistingTovars.Select(x => x.Gender).ToHashSet().ToList();
            AllMechanismType = domain.ExistingTovars.Select(x => x.MechanismType).ToHashSet().ToList();
            AllCaseForm = domain.ExistingTovars.Select(x => x.CaseForm).ToHashSet().ToList();
            AllCaseMaterial = domain.ExistingTovars.Select(x => x.CaseMaterial).ToHashSet().ToList();
            AllGlass = domain.ExistingTovars.Select(x => x.Glass).ToHashSet().ToList();
        }


        public static void CollectPageFilterValues(IEnumerable<Product> Products)
        {
            AllGenders = Products.Select(x => x.Gender).ToHashSet().ToList();
            AllMechanismType = Products.Select(x => x.MechanismType).ToHashSet().ToList();
            AllCaseForm = Products.Select(x => x.CaseForm).ToHashSet().ToList();
            AllCaseMaterial = Products.Select(x => x.CaseMaterial).ToHashSet().ToList();
            AllGlass = Products.Select(x => x.Glass).ToHashSet().ToList();
        }        


        public static void Clear()
        {
            AllGenders = null;
            AllMechanismType = null;
            AllCaseForm = null;
            AllCaseMaterial = null;
            AllGlass = null;
        }


    }




}
