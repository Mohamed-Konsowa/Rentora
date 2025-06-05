using Rentora.Application.Helpers;

namespace Rentora.Presentation.Services
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string email, string message, string subj);
        Task<bool> SendOTPAsync(string email);
        Task<(bool, string)> VerifyOtpAsync(string email, string otpcode);
    }
}
