using AutoMapper;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.DTOs.Identity;

namespace FluentPOS.Modules.Identity.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, FluentRoleClaim>()
                .ForMember(nameof(FluentRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(FluentRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, FluentRoleClaim>()
                .ForMember(nameof(FluentRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(FluentRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}