using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    public class Student
    {
        public int id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public ICollection<Unit> Units { set; get; }
    }
}