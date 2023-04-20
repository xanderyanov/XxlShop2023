using System.Data;

namespace XxlStore.Models
{
    public class User : Domain
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; } = new Role("user");

    }

    public class Role
    {
        public string Name { get; set; }
        public Role(string name) => Name = name;
    }
}
