using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.DTOs.Email;
using Rentora.Presentation.Services;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("sendEmail")]
        public async Task<ActionResult> SendEmail(SendEmailDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var statuscode = await _emailService.SendEmail(model.Email, model.Message, model.Subject);

            if (statuscode == "Accepted")
                return Ok(statuscode);
            return BadRequest(statuscode);
        }

        [HttpPost]
        [Route("send-otp")]
        public async Task<ActionResult> SendOTP([EmailAddress]string email)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _emailService.SendOTP(email);
            if(result)
            return Ok("OTP sent successfully.");
            return BadRequest("Failed to send OTP");
        }

        [HttpGet]
        [Route("verify-otp")]
        public async Task<ActionResult> VerifyOTP(Verify_OTP_DTO vod)
        {
            var result = await _emailService.VerifyOtp(vod.Email, vod.OTPCode);
            if(result.Success) 
                return Ok(result.Data);
            return BadRequest(result.Data);
        }
    }
}
