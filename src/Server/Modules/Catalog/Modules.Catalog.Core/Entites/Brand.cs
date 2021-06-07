using FluentPOS.Shared.Abstractions.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entites
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string LocaleName { get; set; }
        public string ImageUrl { get; set; }
    }
}
