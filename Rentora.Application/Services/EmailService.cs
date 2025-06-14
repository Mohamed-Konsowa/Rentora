﻿using Rentora.Application.Helpers;
using SendGrid.Helpers.Mail;
using SendGrid;
using Rentora.Domain.Models;
using Rentora.Application.IRepositories;
using Rentora.Presentation.Services;
using Microsoft.Extensions.Configuration;
namespace Rentora.Application.Services
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
        public async Task<string> SendEmailAsync(string email, string message, string subj)
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

        public async Task<bool> SendOTPAsync(string email)
        {
            var otpCode = new Random().Next(1000, 9999);
            var result = await SendEmailAsync(email, $"Your OTP : {otpCode}", "Verify your email!");
            if(result == "Accepted")
            {
                var otp = new OTP
                {
                    Email = email,
                    Code = otpCode.ToString(),
                };
                await _unitOfWork.emails.AddOtpAsync(otp);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<(bool, string)> VerifyOtpAsync(string email, string otpcode)
        {
            var otp = await _unitOfWork.emails.GetOtpAsync(email, otpcode);

            if (otp == null)
            {
                return (false, "Invalid OTP or Email!");
            }

            else if (otp.IsUsed)
            {
                return (false, "OTP has already been used!");
            }

            else if (otp.ExpiryTime < DateTime.UtcNow)
            {
                return (false, "OTP has expired!");
            }
            else
            {
                otp.IsUsed = true;
                await _unitOfWork.SaveChangesAsync();
                return (true, "OTP verified successfully!");
            }
        }
    }
}
