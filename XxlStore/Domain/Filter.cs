
namespace XxlStore
{

    public class Filter
    {
        public static List<string> AllGenders { get; private set; }
        
        public static List<string> AllCaseForm { get; private set; }
        
        public static List<string> AllGlass { get; private set; }


        public static void CollectGlobalFilterValues()
        {
            AllGenders = Data.ExistingTovars.Select(x => x.Gender).ToHashSet().ToList();
            AllCaseForm = Data.ExistingTovars.Select(x => x.CaseForm).ToHashSet().ToList();
            AllGlass = Data.ExistingTovars.Select(x => x.Glass).ToHashSet().ToList();
        }


        public static void CollectPageFilterValues(IEnumerable<Product> Products)
        {
            AllGenders = Products.Select(x => x.Gender).ToHashSet().ToList();
            AllCaseForm = Products.Select(x => x.CaseForm).ToHashSet().ToList();
            AllGlass = Products.Select(x => x.Glass).ToHashSet().ToList();
        }        


        public static void Clear()
        {
            AllGenders = null;
            AllCaseForm = null;
            AllGlass = null;
        }


    }




}
