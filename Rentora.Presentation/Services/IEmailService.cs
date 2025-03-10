namespace Rentora.Presentation.Services
{
    public interface IEmailService
    {
        Task<string> SendEmail(string email, string message, string subj);
    }
}
