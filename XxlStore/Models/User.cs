﻿using MongoDB.Bson.Serialization.Attributes;
using System.Data;

namespace XxlStore
{
    public class TUser : Father
    {

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; } = new List<string> { "user" };

        public bool IsActive { get; set; } = true;


        public Role Role { get; set; } = new Role("user");

        [BsonIgnore]
        public bool IsSuperAdmin => Roles.Contains("xander");

        [BsonIgnore]
        public bool IsAdmin => Roles.Contains("admin");        
        
        [BsonIgnore]
        public bool IsContent => Roles.Contains("content");
    }

    public class Role
    {
        public string Name { get; set; }
        public Role(string name) => Name = name;
    }
}
