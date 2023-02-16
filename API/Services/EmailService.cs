using API.Interfaces;
using Microsoft.Extensions.Options;
using MimeKit;
using API.Helpers;
using API.DTOs;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace API.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;
        
        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailDto mailRequest)
        {
            
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            try{
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            } 
            catch (Exception e){
                throw;
            }
        }
    }
}