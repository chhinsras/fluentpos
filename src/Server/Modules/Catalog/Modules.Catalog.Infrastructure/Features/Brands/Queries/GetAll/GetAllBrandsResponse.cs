using System;

namespace FluentPOS.Modules.Catalogs.Infrastructure.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
    }
}
