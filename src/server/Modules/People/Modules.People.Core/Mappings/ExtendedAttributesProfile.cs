using AutoMapper;
using FluentPOS.Shared.Core.Extensions;
using System.Reflection;

namespace FluentPOS.Modules.People.Core.Mappings
{
    public class ExtendedAttributesProfile : Profile
    {
        public ExtendedAttributesProfile()
        {
            this.CreateExtendedAttributesMappings(Assembly.GetExecutingAssembly());
        }
    }
}