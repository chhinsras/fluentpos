using FluentPOS.Shared.Core.Domain;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.EventLogging
{
    public interface IEventLogger
    {
        Task Save<T>(T @event) where T : Event;
    }
}