using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Application
{
    public interface IEntityReferenceService
    {
        public Task TrackAsync(string entityName);
    }
}