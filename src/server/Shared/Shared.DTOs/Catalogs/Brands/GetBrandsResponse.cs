using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Brands
{
    public record GetBrandsResponse(Guid Id, string Name, string Detail);
}