using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CodeFirst.Models
{
    public class ManageDbContext: DbContext
    {
        public ManageDbContext():base("ConnectionDb")
        {

        }
        public DbSet<Student> Students { set; get; }
        public DbSet<Unit> Units { set; get; }
    }
}