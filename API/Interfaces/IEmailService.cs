using API.DTOs;

namespace API.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailDto mailDto);
    }
}