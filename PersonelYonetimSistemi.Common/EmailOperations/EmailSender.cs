using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PersonelYonetimSistemi.Common.EmailOperations
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailKeys _emailKeys;
        public EmailSender(IOptions<EmailKeys> emailKeys)
        {
            _emailKeys = emailKeys.Value;
        }

        public Task SendEmailAsync(string email,string subject,string htmlMessage)
        {
            return Execute(_emailKeys.SendGridApiKey, subject, htmlMessage, email);
        }
        
        private Task Execute(string sendGridApiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridApiKey);
            var from = new EmailAddress("Admin@PersonelYnetimSistemi.com", "Personel Takip Sistemi");
            var to = new EmailAddress(email, "Son Kullanıcı");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            return client.SendEmailAsync(msg);
        }
    }
}
