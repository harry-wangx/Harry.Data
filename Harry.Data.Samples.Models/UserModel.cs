using Harry.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harry.Data.Samples.Models
{
    [Table("Users")]
    public class UserModel : AuditedEntity<int, int>
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public static UserModel User1 { get; } = new UserModel() { Id = 1, Name = "User1" };

        public static UserModel User2 { get; } = new UserModel() { Id = 2, Name = "User2" };
    }
}
