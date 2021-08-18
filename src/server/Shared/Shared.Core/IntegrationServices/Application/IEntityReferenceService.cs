using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Application
{
    public interface IEntityReferenceService
    {
        public Task<string> TrackAsync(string entityName);
    }
}