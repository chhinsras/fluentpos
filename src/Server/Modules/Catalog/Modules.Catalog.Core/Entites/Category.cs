using FluentPOS.Shared.Abstractions.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entites
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Detail { get; set; }
    }
}