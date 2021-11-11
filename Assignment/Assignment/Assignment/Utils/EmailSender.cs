using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Assignment.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, String file,String fileName)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("elvis001351@gmail.com", "HD House Cleaning");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            if (file != null)
            {
                msg.AddAttachment(fileName, file);
            }
            var response = client.SendEmailAsync(msg);
        }

    }
}