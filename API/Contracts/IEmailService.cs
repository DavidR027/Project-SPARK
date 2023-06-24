using API.Repositories;

namespace API.Contracts
{
    public interface IEmailService
    {
        void SendEmailAsync();

        EmailService SetEmail(string email);
        EmailService SetSubject(string subject);
        EmailService SetHtmlMessage(string htmlMessage);
    }
}
