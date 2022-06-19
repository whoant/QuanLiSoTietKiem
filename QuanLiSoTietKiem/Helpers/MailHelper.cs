using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace QuanLiSoTietKiem.Helpers
{
    public static class MailHelper
    {
        public static async Task SendEmail(string receiver, string subject, string message, byte[] bytes = null)
        {
            MailAddress senderEmail = new MailAddress("vovanhoangtuan4.3@gmail.com", "Manage Saving");
            MailAddress receiverEmail = new MailAddress(receiver, "Receiver");
            string password = "xhqgomgunvuobfll";
            string body = message;
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            MailMessage mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
        };
            if (bytes != null)
            {
                mess.Attachments.Add(new Attachment(new MemoryStream(bytes), "bill.pdf"));
            }

            smtp.Send(mess);
            
        }
    }
}