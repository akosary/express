using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Ecommerce.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var FromMail = "mahmoudgamal9622@outlook.com";
            var FromPassword = "Omahm1996oud";

            var Message = new MailMessage();
            Message.From = new MailAddress(FromMail);
            Message.Subject = subject;
            Message.To.Add(email);
            Message.Body = $"<html><body>{htmlMessage}</body></html>";
            Message.IsBodyHtml = true;

            var SmtpClient = new SmtpClient(host: "smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(FromMail, FromPassword),
                EnableSsl = true
            };

            SmtpClient.Send(Message);
        }
    }
}
