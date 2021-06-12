using FluentPOS.Shared.Abstractions.Interfaces.Services;
using FluentPOS.Shared.Abstractions.Settings;
using FluentPOS.Shared.DTOs.Mails;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class SmtpMailService : IMailService
    {
        public MailSettings _settings { get; }
        public ILogger<SmtpMailService> Logger { get; }

        public SmtpMailService(IOptions<MailSettings> settings, ILogger<SmtpMailService> logger)
        {
            _settings = settings.Value;
            Logger = logger;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? _settings.From);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_settings.UserName, _settings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex.Message, ex);
            }
        }
    }
}