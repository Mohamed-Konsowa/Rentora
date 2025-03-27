using Rentora.Application.Helpers;

namespace Rentora.Presentation.Services
{
    public interface IEmailService
    {
        Task<string> SendEmail(string email, string message, string subj);
        Task<bool> SendOTP(string email);
        Task<(bool, string)> VerifyOtp(string email, string otpcode);
    }
}
