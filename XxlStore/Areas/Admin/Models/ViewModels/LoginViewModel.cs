using XxlStore.Areas.Site.Models;

namespace XxlStore.Areas.Admin.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
