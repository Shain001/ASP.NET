namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Appointment")]
    public partial class Appointment
    {
        [Key]

        public int AppId { get; set; }

        public DateTime AppDate { get; set; }

        [Required]
        public string AppAddress { get; set; }

        [Required]
        [StringLength(128)]
        public string UID { get; set; }

        public int TypeId { get; set; }

        public int Rate { get; set; }

        public virtual ServiceType ServiceType { get; set; }
    }
}
