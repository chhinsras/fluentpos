using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Sales.Core.Abstractions;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Sales.Core.Features.Sales.Commands
{
    //Will Return back the Order Id
    public class RegisterSaleCommand : IRequest<Result<Guid>>
    {
        public Guid CartId { get; set; }
    }
    internal sealed class RegisterSaleCommandHandler : IRequestHandler<RegisterSaleCommand, Result<Guid>>
    {
        private readonly ISalesDbContext _salesContext;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<RegisterSaleCommandHandler> _localizer;

        public RegisterSaleCommandHandler(IMapper mapper, IStringLocalizer<RegisterSaleCommandHandler> localizer, ISalesDbContext salesContext)
        {
            _mapper = mapper;
            _localizer = localizer;
            _salesContext = salesContext;
        }

        public async Task<Result<Guid>> Handle(RegisterSaleCommand command, CancellationToken cancellationToken)
        {
            //From CartId
            //Get Customer ID, Use Intergration Services to Get Customer Details
            //Get CartItem Details, Use Intergration Services to Get Product Details
            //Calculate Tax, Total
            //Save to Sales.Order,Transactions and Product
            //Delete CartItem and Cart 
            return default;
        }
    }
}