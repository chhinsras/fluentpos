using FluentPOS.Shared.Application.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entites
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
    }
}