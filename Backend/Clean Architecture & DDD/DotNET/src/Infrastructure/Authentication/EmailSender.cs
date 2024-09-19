using Application.Common.Interfaces.EmailService;
using Infrastructure.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SharedKernel.Email;

namespace Infrastructure.Authentication;

public sealed class EmailSender : IEmailSender<IdentityUser<int>>
{
    private readonly IEmailService _emailService;
    private readonly EmailSettings _emailSettings;

    public EmailSender(
        IEmailService emailService,
        IOptions<EmailSettings> emailSettings)
    {
        _emailService = emailService;
        _emailSettings = emailSettings.Value;
    }

    public Task SendConfirmationLinkAsync(IdentityUser<int> user, string email, string confirmationLink)
    {
        var emailResult = EmailData.Create(
            to: [email],
            subject: "Confirmation Link",
            textContent: confirmationLink);

        _emailService.SendEmail(emailResult.Value);

        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(IdentityUser<int> user, string email, string resetCode)
    {
        var url = $"{_emailSettings.BaseUrl}/#/password-reset?email={email}&resetCode={resetCode}";

        var emailResult = EmailData.Create(
            to: [email],
            subject: "Password Reset",
            htmlContent: $"<a href=\"{url}\">Password Reset</a>");

        _emailService.SendEmail(emailResult.Value);

        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(IdentityUser<int> user, string email, string resetLink)
    {
        var emailResult = EmailData.Create(
            to: [email],
            subject: "Password Reset Link",
            textContent: resetLink);

        _emailService.SendEmail(emailResult.Value);

        return Task.CompletedTask;
    }
}
