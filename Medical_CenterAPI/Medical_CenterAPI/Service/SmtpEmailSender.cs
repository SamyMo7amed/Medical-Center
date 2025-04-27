using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
namespace Medical_CenterAPI.Service
{
    public class SmtpEmailSender :IEmailSender
    {

        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtp = new SmtpClient(_config["Smtp:Host"], int.Parse(_config["Smtp:Port"]!));
            smtp.Credentials = new NetworkCredential(_config["Smtp:Username"], _config["Smtp:Password"]);
            smtp.EnableSsl=true;
            var msg = new MailMessage(_config["Smtp:From"]!, email, subject, htmlMessage) { IsBodyHtml = true };  

            await smtp.SendMailAsync(msg);
        }
    }
}
