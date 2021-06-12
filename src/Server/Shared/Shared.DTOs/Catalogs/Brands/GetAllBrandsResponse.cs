using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Brands
{
    public record GetAllBrandsResponse(Guid Id, string Name, string Detail);
}