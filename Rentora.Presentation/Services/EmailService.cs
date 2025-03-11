using Rentora.Persistence.Helpers;
using SendGrid.Helpers.Mail;
using SendGrid;
using Rentora.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Rentora.Application.IRepositories;
using Microsoft.AspNetCore.Mvc;
namespace Rentora.Presentation.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> SendEmail(string email, string message, string subj)
        {
            var sendGridOptions = _configuration.GetSection("SendGrid").Get<SendGridOptions>();
            var apiKey = sendGridOptions.ApiKey; // API Key
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(sendGridOptions.SenderEmail, sendGridOptions.SenderName); // sender
            var subject = subj;
            var to = new EmailAddress(email); // reciever

            var msg = new SendGridMessage()
            {
                From = from,
                Subject = subject,
                PlainTextContent = $"Hello, this is an email sent from {sendGridOptions.SenderName} using SendGrid!",
                HtmlContent = $"<strong>{message}</strong>"
            };

            msg.AddTo(to);

            //msg.ReplyTo = new MailAddress("reply-to@example.com");

            var response = await client.SendEmailAsync(msg);

            return response.StatusCode.ToString();
        }

        public async Task<bool> SendOTP(string email)
        {
            var otpCode = new Random().Next(1000, 9999);
            var result = await SendEmail(email, $"Your OTP : {otpCode}", "Verify your email!");
            if(result == "Accepted")
            {
                var otp = new OTP
                {
                    Email = email,
                    Code = otpCode.ToString(),
                };
                await _unitOfWork.emails.AddOtp(otp);
                await _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<CustomResponse<string>> VerifyOtp(string email, string otpcode)
        {
            var response = new CustomResponse<string>();
            var otp = await _unitOfWork.emails.GetOtp(email, otpcode);

            if (otp == null)
            {
                response.Success = false;
                response.Data = "Invalid OTP or Email!";
            }

            else if (otp.IsUsed)
            {
                response.Success = false;
                response.Data = "OTP has already been used!";
            }

            else if (otp.ExpiryTime < DateTime.UtcNow)
            {
                response.Success = false;
                response.Data = "OTP has expired!";
            }
            else
            {
                response.Success = true;
                response.Data = "OTP verified successfully!";
                otp.IsUsed = true;
                await _unitOfWork.Save();
            }
            
            return response;
        }
    }
}
