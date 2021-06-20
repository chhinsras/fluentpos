#nullable enable
using System;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands
{
    public class UpdateExtendedAttributeCommand<TEntity> : IRequest<Result<Guid>>
        where TEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid EntityId { get; set; }
        public ExtendedAttributeType Type { get; set; }
        public string Key { get; set; }
        public decimal? Decimal { get; set; }
        public string? Text { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Json { get; set; }
        public string? ExternalId { get; set; }
        public string? Group { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}