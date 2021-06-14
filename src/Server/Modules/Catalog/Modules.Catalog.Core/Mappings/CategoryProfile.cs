using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalog.Core.Features.Categories.Commands;
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
        }
    }
}
