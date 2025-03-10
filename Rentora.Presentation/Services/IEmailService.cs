using Rentora.Persistence.Helpers;

namespace Rentora.Presentation.Services
{
    public interface IEmailService
    {
        Task<string> SendEmail(string email, string message, string subj);
        Task<bool> SendOTP(string email);
        Task<CustomResponse<string>> VerifyOtp(string email, string otpcode);
    }
}
