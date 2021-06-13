using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Shared.Application.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetBrandImageQuery : IRequest<Result<string>>
    {
        public Guid Id { get; set; }

        public GetBrandImageQuery(Guid brandId)
        {
            Id = brandId;
        }
    }
}