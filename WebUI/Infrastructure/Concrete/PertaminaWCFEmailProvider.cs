using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Infrastructure.Abstract;
using WebUI.ServiceReferenceEmail;

namespace WebUI.Infrastructure.Concrete
{
    public class PertaminaWCFEmailProvider : IEmailProvider
    {
        public ResponseMessage SendEmail(string to, string cc, string bcc, string subject, string content)
        {
            WebUI.ServiceReferenceEmail.GeneralClient gc = new GeneralClient();
            WebUI.ServiceReferenceEmail.Message_Request mrequest = new Message_Request();
            WebUI.ServiceReferenceEmail.Message_Response mresponse = new Message_Response();
            WebUI.ServiceReferenceEmail.ResponseMessage rmessage = new ResponseMessage();

            mrequest.TypeMessage = "1";
            mrequest.To = to;
            mrequest.Cc = cc;
            mrequest.Bcc = bcc;
            mrequest.Subject = subject;
            mrequest.Content = content;
            mrequest.DatetimeCreated = DateTime.Now.ToString("yyyyMMddssmmHH");

            WebUI.ServiceReferenceEmail.Common_Request cr = new Common_Request();

            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            cr.UserName = "mobidig";
            cr.Password = "m0b1d1g@pertamina";
            mresponse = gc.Send_Message(cr, mrequest, out rmessage);

            return rmessage;
        }
    }
}