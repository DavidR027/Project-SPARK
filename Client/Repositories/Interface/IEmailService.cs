using Client.Repositories.Data;

namespace Client.Repositories.Interface
{
    public interface IEmailService
    {
        void SendEmailAsync();

        EmailService SetEmail(string email);
        EmailService SetSubject(string subject);
        EmailService SetHtmlMessage(string htmlMessage);
    }
}
