using FluentPOS.Shared.DTOs.Sms;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface ISmsService
    {
        Task SendAsync(SmsRequest request);
    }
}