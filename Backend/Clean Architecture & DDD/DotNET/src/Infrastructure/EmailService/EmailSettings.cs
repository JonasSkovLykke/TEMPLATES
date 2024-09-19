namespace Infrastructure.EmailService;

public sealed class EmailSettings
{
    public const string SectionName = "EmailSettings";

    private string apiKey = null!;

    public string ApiKey
    {
        get { return Environment.GetEnvironmentVariable("email-settings-api-key") ?? apiKey; }
        set { apiKey = value; }
    }

    private string fromEmail = null!;

    public string FromEmail
    {
        get { return Environment.GetEnvironmentVariable("email-settings-from-email") ?? fromEmail; }
        set { fromEmail = value; }
    }

    private string fromName = null!;

    public string FromName
    {
        get { return Environment.GetEnvironmentVariable("email-settings-from-name") ?? fromName; }
        set { fromName = value; }
    }

    private string baseUrl = null!;

    public string BaseUrl
    {
        get { return Environment.GetEnvironmentVariable("email-settings-base-url") ?? baseUrl; }
        set { baseUrl = value; }
    }

    #region Development
    public string? Host { get; set; }
    public int Port { get; set; }
    #endregion
}
