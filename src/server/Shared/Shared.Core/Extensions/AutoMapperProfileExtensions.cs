using AutoMapper;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using System.Linq;
using System.Reflection;

namespace FluentPOS.Shared.Core.Extensions
{
    public static class AutoMapperProfileExtensions
    {
        public static Profile CreateExtendedAttributesMappings(this Profile profile, Assembly assembly)
        {
            var extendedAttributeTypes = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ExtendedAttribute<,>))
                .ToList();

            foreach (var extendedAttributeType in extendedAttributeTypes)
            {
                var extendedAttributeTypeGenericArguments = extendedAttributeType.BaseGenericType.GetGenericArguments().ToList();

                #region AddExtendedAttributeCommand

                var sourceType = typeof(AddExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                profile.CreateMap(sourceType, extendedAttributeType.CurrentType).ReverseMap();

                #endregion AddExtendedAttributeCommand

                #region UpdateExtendedAttributeCommand

                sourceType = typeof(UpdateExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                profile.CreateMap(sourceType, extendedAttributeType.CurrentType).ReverseMap();

                #endregion UpdateExtendedAttributeCommand

                #region GetExtendedAttributeByIdResponse

                sourceType = typeof(GetExtendedAttributeByIdResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                profile.CreateMap(sourceType, extendedAttributeType.CurrentType).ReverseMap();

                #endregion GetExtendedAttributeByIdResponse

                #region GetExtendedAttributesResponse

                sourceType = typeof(GetExtendedAttributesResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                profile.CreateMap(sourceType, extendedAttributeType.CurrentType).ReverseMap();

                #endregion GetExtendedAttributesResponse
            }

            return profile;
        }
    }
}