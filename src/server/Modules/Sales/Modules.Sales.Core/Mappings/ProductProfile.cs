using AutoMapper;
using FluentPOS.Modules.Sales.Core.Entities;
using FluentPOS.Shared.DTOs.Catalogs.Products;

namespace FluentPOS.Modules.Sales.Core.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetProductByIdResponse, Product>().ReverseMap();
        }
    }
}