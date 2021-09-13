using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment_Auth.Models
{
    public class Appointment
    {
        public int AppId { get; set; }
        public string Address { get; set; }
        public DateTime AppDateTime { get; set; }
        public string ContactNumber { get; set; }
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
        public int UId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}