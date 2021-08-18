using System;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.IntegrationServices.Inventory
{
    public interface IStockService
    {
        public Task RecordTransaction(Guid productId, decimal quantity, string referenceNumber, bool isSale = true);
    }
}