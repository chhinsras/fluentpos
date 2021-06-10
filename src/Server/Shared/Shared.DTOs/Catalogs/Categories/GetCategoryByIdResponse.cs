using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Categories
{
    public class GetCategoryByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
    }
}