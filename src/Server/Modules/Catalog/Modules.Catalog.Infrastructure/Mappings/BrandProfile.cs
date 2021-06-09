using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalogs.Infrastructure.Features.Brands.Commands.AddEdit;
using FluentPOS.Modules.Catalogs.Infrastructure.Features.Brands.Queries.GetAll;
using FluentPOS.Modules.Catalogs.Infrastructure.Features.Brands.Queries.GetById;

namespace FluentPOS.Modules.Catalogs.Infrastructure.Mappings
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<AddEditBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllBrandsResponse, Brand>().ReverseMap();
        }
    }
}
