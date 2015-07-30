using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Alfasoft.Models
{
    public class AppContext : DbContext
    {
        public AppContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<User> User { get; set; }
    }
}