using Application.Models.Email;

namespace Application.Core.Abstractions.Emails;
public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}
