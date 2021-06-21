using System.Reflection;
using AutoMapper;
using FluentPOS.Shared.Core.Extensions;

namespace FluentPOS.Modules.Identity.Core.Mappings
{
    public class ExtendedAttributesProfile : Profile
    {
        public ExtendedAttributesProfile()
        {
            this.CreateExtendedAttributesMappings(Assembly.GetExecutingAssembly());
        }
    }
}