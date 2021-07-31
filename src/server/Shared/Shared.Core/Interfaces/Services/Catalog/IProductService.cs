using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;

namespace FluentPOS.Shared.Core.Interfaces.Services.Catalog
{
    public interface IProductService
    {
        Task<Result<GetProductByIdResponse>> GetDetails(Guid productId);
    }
}