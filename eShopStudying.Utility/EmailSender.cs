using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace eShopStudying.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // var emailToSend = new MimeMessage();
            // emailToSend.From.Add(MailboxAddress.Parse("eShopStudying"));
            // emailToSend.To.Add(MailboxAddress.Parse(email));
            // emailToSend.Subject = subject;
            // emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage};

            // using (var client = new SmtpClient())
            // {
            //     client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //     client.Authenticate("email", "password");
            //     client.Send(emailToSend);
            //     client.Disconnect(true);
            // }

            return Task.CompletedTask;
        }
    }
}