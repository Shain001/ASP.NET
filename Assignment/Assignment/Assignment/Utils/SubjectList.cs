using Assignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Utils
{
    public static class SubjectList
    {

        public static List<SelectListItem> Emails { get; set; }
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

        public static IEnumerable<SelectListItem> SubjectItemsAdmin()
        {
            var itmes = new List<SelectListItem>
            {
                new SelectListItem() {Text="Notification", Value = "Notification"},
                new SelectListItem() {Text="About Appointment", Value="About Appointment"},
                new SelectListItem() {Text="General", Value="General"}

            };
            return itmes;
        }

        public static IEnumerable<SelectListItem> GetUserEmails()
        {
            var context = new IdentityDbContext();

            Emails = new List<SelectListItem>();
            Emails.Add(new SelectListItem() { Value = "-1", Text = "All User" });

            var users = context.Users.ToList();

            foreach(var u in users)
            {
                Emails.Add(new SelectListItem() { Value = u.Email, Text = u.Email });
            }

            



            return Emails;
        }
    }
}