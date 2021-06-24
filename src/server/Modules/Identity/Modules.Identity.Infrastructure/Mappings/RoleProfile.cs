using AutoMapper;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.DTOs.Identity.Roles;

namespace FluentPOS.Modules.Identity.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, FluentRole>().ReverseMap();
        }
    }
}