using SharedKernel.Email;

namespace Application.Common.Interfaces.EmailService;

public interface IEmailService
{
    public void SendEmail(EmailData emailData);
}
