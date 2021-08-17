using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace FIT5032_CodeFirst.Models
{
    public class DbStudentUnitsContext:DbContext
    {
        public DbSet<Student> Students { set; get; }
        public DbSet<Unit> Units { set; get; }

  
    }
}