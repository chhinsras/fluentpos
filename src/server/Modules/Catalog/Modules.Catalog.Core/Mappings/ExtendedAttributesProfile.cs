using System.Reflection;
using AutoMapper;
using FluentPOS.Shared.Core.Extensions;

namespace FluentPOS.Modules.Catalog.Core.Mappings
{
    public class ExtendedAttributesProfile : Profile
    {
        public ExtendedAttributesProfile()
        {
            this.CreateExtendedAttributesMappings(Assembly.GetExecutingAssembly());
        }
    }
}