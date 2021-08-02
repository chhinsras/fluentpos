using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Categories
{
    public record GetCategoriesResponse(Guid Id, string Name, string Detail);
}