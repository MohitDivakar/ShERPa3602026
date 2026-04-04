using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace ShERPa360net.Models
{
    public static class EmailSend
    {
        public static bool EmailSent(string message, string subject, string senderusername, string senderpassword, string receiveremail)
        {
            try
            {
                bool IsSent = false;
                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("customercare@qarmatek.com", "Mobex.in");
                mail.To.Add(receiveremail);
                //mail.CC.Add("kushal.shah@qarmatek.com");
                mail.CC.Add("nipun.shah@qarmatek.com");
                mail.CC.Add("sohil.raj@qarmatek.com");
                //mail.CC.Add("sohil.raj@qarmatek.com");

                //mail.CC.Add("ketan.patel@qarmatek.com");
                //mail.CC.Add("mohit.diwakar@qarmatek.com");
                //mail.CC.Add("jpvaishnav@qarmatek.com");
                //mail.Bcc.Add("sentitem@qarmatek.com");
                mail.Subject     = subject;
                mail.Body        = message;
                mail.IsBodyHtml  = true;
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Port = 587;

                //SmtpServer.Port = 25;
                //SmtpServer.UseDefaultCredentials = true;

                //extra line
                SmtpServer.Host           = "smtp.office365.com";
                //SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Credentials    = new System.Net.NetworkCredential("customercare@qarmatek.com", "Saz43287");
                SmtpServer.EnableSsl      = true;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Send(mail);
                IsSent                    = true;
                return IsSent;
            }
            catch (Exception ex)
            {
                string errormssage = ex.Message;
                //SendSMS.SMSSent(errormssage, "8160003453");
                return false;
            }
        }
    }
}