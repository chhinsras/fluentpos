using FluentPOS.Shared.DTOs.Mails;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Abstractions.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}