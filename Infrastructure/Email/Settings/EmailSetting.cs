namespace Infrastructure.Email.Settings;
public class EmailSetting
{
    public const string SettingsKey = "Mail";

    public string SenderDisplayName { get; set; }

    public string SenderEmail { get; set; }

    public string SmtpPassword { get; set; }

    public string SmtpServer { get; set; }

    public int SmtpPort { get; set; }
}

