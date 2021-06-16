using FluentPOS.Shared.Application.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands
{
    public class RemoveBrandCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveBrandCommand(Guid brandId)
        {
            Id = brandId;
        }
    }
}