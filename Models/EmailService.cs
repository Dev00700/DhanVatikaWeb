using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace DhanVatikaWeb.Models
{
    public class EmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendMail(string toEmail, string subject, string body)
        {
            var smtp = new SmtpClient
            {
                Host = _config["EmailSettings:SmtpServer"],
                Port = int.Parse(_config["EmailSettings:Port"]),
                EnableSsl = true,
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Username"],
                    _config["EmailSettings:Password"]
                )
            };

            var mail = new MailMessage
            {
                From = new MailAddress(
                    _config["EmailSettings:SenderEmail"],
                    _config["EmailSettings:SenderName"]
                ),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(toEmail);
            smtp.Send(mail);
        }

      

    }
}
