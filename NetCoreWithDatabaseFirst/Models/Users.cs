using System;
using System.Collections.Generic;

namespace NetCoreWithDatabaseFirst.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Status { get; set; }
    }
}
