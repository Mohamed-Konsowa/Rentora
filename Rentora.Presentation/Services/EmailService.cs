using Rentora.Persistence.Helpers;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Rentora.Presentation.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
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
                PlainTextContent = "Hello, this is an email sent from .NET using SendGrid!",
                HtmlContent = $"<strong>{message}</strong>"
            };

            msg.AddTo(to);

            //msg.ReplyTo = new MailAddress("reply-to@example.com");

            var response = (SendGrid.Response)await client.SendEmailAsync(msg);

            return response.StatusCode.ToString();
        }
    }
}
