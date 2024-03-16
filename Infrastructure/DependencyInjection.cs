using Application.Core.Abstractions.Common;
using Application.Core.Abstractions.Emails;
using Infrastructure.Authentication.Settings;
using Infrastructure.Common;
using Infrastructure.Email;
using Infrastructure.Email.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSetting>(configuration.GetSection(EmailSetting.SettingsKey));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IDateTime, MachineDateTime>();
        return services;
    }
}
