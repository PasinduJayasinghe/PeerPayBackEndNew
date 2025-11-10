using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            // TODO: Implement actual email sending (SMTP, SendGrid, etc.)
            // For now, just log the email
            _logger.LogInformation($"Sending email to {to}");
            _logger.LogInformation($"Subject: {subject}");
            _logger.LogInformation($"Body: {body}");

            // Simulate async operation
            await Task.Delay(100);

            return true;
        }

        public async Task<bool> SendOtpEmailAsync(string to, string otpCode)
        {
            var subject = "PeerPay.lk - Email Verification OTP";
            var body = $@"
                <html>
                <body>
                    <h2>Email Verification</h2>
                    <p>Your OTP code for PeerPay.lk registration is:</p>
                    <h1 style='color: #8C00FF; font-size: 32px; letter-spacing: 5px;'>{otpCode}</h1>
                    <p>This code will expire in 10 minutes.</p>
                    <p>If you didn't request this code, please ignore this email.</p>
                    <br>
                    <p>Best regards,<br>PeerPay.lk Team</p>
                </body>
                </html>
            ";

            return await SendEmailAsync(to, subject, body);
        }
    }
}
