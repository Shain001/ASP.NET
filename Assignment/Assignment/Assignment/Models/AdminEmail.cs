using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Models
{
    public class AdminEmail
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Please enter an email address.")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public IEnumerable<string> ToEmail { get; set; }

        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter the contents")]
        public string Contents { get; set; }

        // attachment
        public HttpPostedFileBase Attatchment { get; set; }
    }
}