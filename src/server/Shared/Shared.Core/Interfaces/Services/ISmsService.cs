using System.Threading.Tasks;
using FluentPOS.Shared.DTOs.Sms;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface ISmsService
    {
        Task SendAsync(SmsRequest request);
    }
}