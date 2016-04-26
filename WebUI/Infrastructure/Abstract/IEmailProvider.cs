using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.ServiceReferenceEmail;

namespace WebUI.Infrastructure.Abstract
{
    public interface IEmailProvider
    {
        ResponseMessage SendEmail(string to, string cc, string bcc, string subject, string content);
    }
}