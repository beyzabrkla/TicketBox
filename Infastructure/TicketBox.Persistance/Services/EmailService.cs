using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using TicketBox.Application.Interfaces;

namespace TicketBox.Persistance.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        // Configurationı constructor üzerinden inject ediyoruz
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendTicketEmailAsync(string toEmail, string subject, string body)
        {
            // appsettings.json dosyasından verileri oku
            var email = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];
            var host = _configuration["EmailSettings:Host"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            var fromAddress = new MailAddress(email, "TicketBox Destek");

            var mailMessage = new MailMessage(fromAddress, new MailAddress(toEmail))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}