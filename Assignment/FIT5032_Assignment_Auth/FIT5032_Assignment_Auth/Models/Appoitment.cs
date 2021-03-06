namespace FIT5032_Assignment_Auth.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appoitment")]
    public partial class Appoitment
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Appointment Date")]
        public DateTime AppDate { get; set; }

        [Required]
        public string Address { get; set; }

        public int Postcode { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }

        public int? Rate { get; set; }

        [StringLength(128)]

        [ForeignKey("User")]
        public string Uid { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
