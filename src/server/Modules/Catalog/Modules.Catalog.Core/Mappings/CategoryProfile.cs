using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Commands;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Queries;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.DTOs.Catalogs.Categories;

namespace FluentPOS.Modules.Catalog.Core.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<RegisterCategoryCommand, Category>().ReverseMap();
            CreateMap<UpdateCategoryCommand, Category>().ReverseMap();
            CreateMap<GetCategoryByIdResponse, Category>().ReverseMap();
            CreateMap<GetAllPagedCategoriesResponse, Category>().ReverseMap();
            CreateMap<PaginatedCategoryFilter, GetAllPagedCategoriesQuery>()
                .ForMember(dest => dest.OrderBy, opt => opt.ConvertUsing<string>(new OrderByConverter()));
        }
    }
}