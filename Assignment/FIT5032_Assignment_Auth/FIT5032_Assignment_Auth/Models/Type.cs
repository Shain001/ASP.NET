using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_Assignment_Auth.Models
{
    public class Type
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public virtual ICollection<Appoitment> Appoitment { set; get; }
    }
}