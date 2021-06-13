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
            CreateMap<RegisterBrandCommand, Brand>().ReverseMap();
            CreateMap<UpdateBrandCommand, Brand>().ReverseMap();
            CreateMap<GetBrandByIdResponse, Brand>().ReverseMap();
            CreateMap<GetAllPagedBrandsResponse, Brand>().ReverseMap();
        }
    }
}
