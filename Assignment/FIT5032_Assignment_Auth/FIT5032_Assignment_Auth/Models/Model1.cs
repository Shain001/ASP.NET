using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment_Auth.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Appointment_Model")
        {
        }

        public virtual DbSet<Appoitment> Appoitment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
