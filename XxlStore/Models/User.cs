﻿using MongoDB.Bson.Serialization.Attributes;
using System.Data;

namespace XxlStore.Models
{
    public class User : Domain
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; } = new List<string> { "user" };


        public Role Role { get; set; } = new Role("user");

        [BsonIgnore]
        public bool IsAdmin => Roles.Contains("admin");
    }

    public class Role
    {
        public string Name { get; set; }
        public Role(string name) => Name = name;
    }
}
