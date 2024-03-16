using Application.Core.Abstractions.Emails;
using Application.Models.Email;
using Infrastructure.Email.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Email;
internal class EmailService : IEmailService
{
    private readonly EmailSetting _mailSettings;

    public EmailService(IOptions<EmailSetting> maiLSettingsOptions) => _mailSettings = maiLSettingsOptions.Value;
    public Task SendEmailAsync(MailRequest mailRequest)
    {
        return Task.FromResult(true);
    }
}
