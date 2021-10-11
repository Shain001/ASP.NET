using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Utils
{
    public static class SubjectList
    {
        public static IEnumerable<SelectListItem> SubjectItems()
        {
            var itmes = new List<SelectListItem>
            {
                new SelectListItem() {Text="Complaint", Value = "Complaint"},
                new SelectListItem() {Text="Feedback", Value="Feedback"},
                new SelectListItem() {Text="General Question", Value="General Question"}

            };
            return itmes;
        }
    }
}