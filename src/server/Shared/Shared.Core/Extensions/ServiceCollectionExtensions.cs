using System;
using System.Linq;
using FluentPOS.Shared.Core.Behaviors;
using FluentPOS.Shared.Core.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;

namespace FluentPOS.Shared.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CacheSettings>(config.GetSection(nameof(CacheSettings)));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

        public static IServiceCollection AddExtendedAttributeHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var extendedAttributeTypes = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ExtendedAttribute<>))
                .ToList();

            foreach (var extendedAttributeType in extendedAttributeTypes)
            {
                var extendedAttributeTypeGenericArguments = extendedAttributeType.BaseGenericType.GetGenericArguments().ToList();
                var queriesImplementationType = typeof(ExtendedAttributeQueryHandler<,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeType.CurrentType);
                var commandsImplementationType = typeof(ExtendedAttributeCommandHandler<,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeType.CurrentType);

                #region GetAllPagedExtendedAttributesQuery

                var tRequest = typeof(GetAllPagedExtendedAttributesQuery<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                var tResponse = typeof(PaginatedResult<>).MakeGenericType(typeof(GetAllPagedExtendedAttributesResponse));
                var serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, queriesImplementationType);

                #endregion GetAllPagedExtendedAttributesQuery

                #region GetExtendedAttributeByIdQuery

                tRequest = typeof(GetExtendedAttributeByIdQuery<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                tResponse = typeof(Result<>).MakeGenericType(typeof(GetExtendedAttributeByIdResponse));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, queriesImplementationType);

                #endregion GetExtendedAttributeByIdQuery

                #region AddExtendedAttributeCommand

                tRequest = typeof(AddExtendedAttributeCommand<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion AddExtendedAttributeCommand

                #region UpdateExtendedAttributeCommand

                tRequest = typeof(UpdateExtendedAttributeCommand<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion UpdateExtendedAttributeCommand

                #region RemoveExtendedAttributeCommand

                tRequest = typeof(RemoveExtendedAttributeCommand<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]);
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion RemoveExtendedAttributeCommand
            }

            return services;
        }
    }
}