using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Brands
{
    public class GetBrandByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
    }
}