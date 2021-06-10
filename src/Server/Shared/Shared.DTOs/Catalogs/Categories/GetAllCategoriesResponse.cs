using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Categories
{
    public record GetAllCategoriesResponse(Guid Id, string Name, string Detail);
}