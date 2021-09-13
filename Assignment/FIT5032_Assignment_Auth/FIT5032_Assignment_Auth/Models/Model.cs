using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment_Auth.Models
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=Modeltest")
        {
        }

        public virtual DbSet<Appoitment> Appoitment { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
