using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FIT5032_CodeFirst.Models
{
    public class Student
    {
        
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public ICollection<Unit> Units { get; set; }
    }
}