using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;

namespace FluentPOS.Shared.Core.IntegrationServices.People
{
    public interface ICartService
    {
        Task<Result<GetCartByIdResponse>> GetDetails(Guid cartId);
    }
}