using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using WebUI.Infrastructure.Abstract;
using WebUI.ServiceReferenceEmail;

namespace WebUI.Infrastructure.Concrete
{
    public class GmailEmailProvider : IEmailProvider
    {
        public ResponseMessage SendEmail(string to, string cc, string bcc, string subject, string content)
        {
            var message = new MailMessage();
            List<string> toList = new List<string>();

            toList = to.Split(',').ToList();
            foreach (string row in toList)
            {
                message.To.Add(new MailAddress(row));
            }
            message.Subject = subject;
            message.Body = content;
            message.From = new MailAddress("rekadia.id@gmail.com");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
            }

            return null;
        }
    }
}