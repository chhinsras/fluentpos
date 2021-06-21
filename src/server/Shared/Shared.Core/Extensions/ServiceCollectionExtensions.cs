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
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Events;
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
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(ExtendedAttribute<,>))
                .ToList();

            foreach (var extendedAttributeType in extendedAttributeTypes)
            {
                var extendedAttributeTypeGenericArguments = extendedAttributeType.BaseGenericType.GetGenericArguments().ToList();
                var queriesImplementationType = typeof(ExtendedAttributeQueryHandler<,,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1], extendedAttributeType.CurrentType);
                var commandsImplementationType = typeof(ExtendedAttributeCommandHandler<,,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1], extendedAttributeType.CurrentType);
                var eventsImplementationType = typeof(ExtendedAttributeEventHandler<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());

                #region GetAllPagedExtendedAttributesQuery

                var tRequest = typeof(GetAllPagedExtendedAttributesQuery<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                var tResponse = typeof(PaginatedResult<>).MakeGenericType(typeof(GetAllPagedExtendedAttributesResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]));
                var serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, queriesImplementationType);

                #endregion GetAllPagedExtendedAttributesQuery

                #region GetExtendedAttributeByIdQuery

                tRequest = typeof(GetExtendedAttributeByIdQuery<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(GetExtendedAttributeByIdResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, queriesImplementationType);

                #endregion GetExtendedAttributeByIdQuery

                #region AddExtendedAttributeCommand

                tRequest = typeof(AddExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion AddExtendedAttributeCommand

                #region UpdateExtendedAttributeCommand

                tRequest = typeof(UpdateExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion UpdateExtendedAttributeCommand

                #region RemoveExtendedAttributeCommand

                tRequest = typeof(RemoveExtendedAttributeCommand<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                tResponse = typeof(Result<>).MakeGenericType(typeof(Guid));
                serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, commandsImplementationType);

                #endregion RemoveExtendedAttributeCommand

                #region ExtendedAttributeAddedEvent

                serviceType = typeof(INotificationHandler<>).MakeGenericType(typeof(ExtendedAttributeAddedEvent<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray()));
                services.AddScoped(serviceType, eventsImplementationType);

                #endregion ExtendedAttributeAddedEvent

                #region ExtendedAttributeUpdatedEvent

                serviceType = typeof(INotificationHandler<>).MakeGenericType(typeof(ExtendedAttributeUpdatedEvent<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray()));
                services.AddScoped(serviceType, eventsImplementationType);

                #endregion ExtendedAttributeUpdatedEvent

                #region ExtendedAttributeRemovedEvent

                serviceType = typeof(INotificationHandler<>).MakeGenericType(typeof(ExtendedAttributeRemovedEvent<>).MakeGenericType(extendedAttributeTypeGenericArguments[1]));
                services.AddScoped(serviceType, eventsImplementationType);

                #endregion ExtendedAttributeRemovedEvent
            }

            return services;
        }
    }
}