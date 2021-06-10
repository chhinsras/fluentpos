using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalog.Core.Features.Brands.Commands;
using FluentPOS.Shared.DTOs.Catalogs.Brands;

namespace FluentPOS.Modules.Catalog.Core.Mappings
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
