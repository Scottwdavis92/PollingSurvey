using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PollingApplication.Models;

namespace PollingApplication.Services
{
    public class Mailer
    {
        private readonly ILogger<SurveyInvite> _logger;
        private readonly IConfiguration _config;

        public Mailer(ILogger<SurveyInvite> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<bool> SendEmailAsync(SurveySessionId survey, string? surveyLink)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender Name", "CompanyPolling@survey.com"));
                message.To.Add(new MailboxAddress("Recipient Name", survey.Employee.Email));
                message.Subject = "Hello from Scott's Survey!";

                var emailbody = $"Dear {survey.Employee.FirstName},\n\n" +
                                "Please take a moment to complete the Company survey by clicking the link below:\n\n" +
                                $"{surveyLink}\n\n" +
                                "Thank you for your participation!\n\n" +
                                "Best regards,\n" +
                                "PollAdmin";

                // Create the email body (can be plain text or HTML)
                message.Body = new TextPart("plain")
                {
                    Text = emailbody,
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_config["Smtp:Host"], 587, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_config["Smtp:Username"], _config["Smtp:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {survey.Employee.Email}.");
                return false;
            }
        }
    }
}
