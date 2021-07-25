using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries
{
    internal class CartQueryHandler :
        IRequestHandler<GetCartByIdQuery, Result<GetCartByIdResponse>>,
        IRequestHandler<GetCartByCustomerIdQuery, Result<GetCartByCustomerIdResponse>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CartQueryHandler> _localizer;

        public CartQueryHandler(IPeopleDbContext context, IMapper mapper, IStringLocalizer<CartQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<GetCartByIdResponse>> Handle(GetCartByIdQuery query, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts.Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (cart == null) throw new PeopleException(_localizer["Cart Not Found!"]);
            var mappedCart = _mapper.Map<GetCartByIdResponse>(cart);
            return await Result<GetCartByIdResponse>.SuccessAsync(mappedCart);
        }

        public async Task<Result<GetCartByCustomerIdResponse>> Handle(GetCartByCustomerIdQuery query, CancellationToken cancellationToken)
        {
            var cart = await _context.Carts.Where(c => c.CustomerId == query.CustomerId).FirstOrDefaultAsync(cancellationToken);
            if (cart == null) throw new PeopleException(_localizer["Cart Not Found!"]);
            var mappedCart = _mapper.Map<GetCartByCustomerIdResponse>(cart);
            return await Result<GetCartByCustomerIdResponse>.SuccessAsync(mappedCart);
        }
    }
}