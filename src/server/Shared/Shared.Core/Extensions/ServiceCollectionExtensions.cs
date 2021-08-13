// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using FluentPOS.Shared.Core.Behaviors;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Events;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Serialization;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        public static IServiceCollection AddSerialization(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SerializationSettings>(config.GetSection(nameof(SerializationSettings)));
            var options = services.GetOptions<SerializationSettings>(nameof(SerializationSettings));
            services.AddSingleton<IJsonSerializerSettingsOptions, JsonSerializerSettingsOptions>();
            if (options.UseSystemTextJson)
            {
                services
                    .AddSingleton<IJsonSerializer, SystemTextJsonSerializer>()
                    .Configure<JsonSerializerSettingsOptions>(configureOptions =>
                    {
                        if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        {
                            configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                        }
                    });
            }
            else if (options.UseNewtonsoftJson)
            {
                services
                    .AddSingleton<IJsonSerializer, NewtonSoftJsonSerializer>();
            }

            return services;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName)
            where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection(sectionName);
            var options = new T();
            section.Bind(options);

            return options;
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

                #region GetExtendedAttributesQuery

                var tRequest = typeof(GetExtendedAttributesQuery<,>).MakeGenericType(extendedAttributeTypeGenericArguments.ToArray());
                var tResponse = typeof(PaginatedResult<>).MakeGenericType(typeof(GetExtendedAttributesResponse<>).MakeGenericType(extendedAttributeTypeGenericArguments[0]));
                var serviceType = typeof(IRequestHandler<,>).MakeGenericType(tRequest, tResponse);
                services.AddScoped(serviceType, queriesImplementationType);

                #endregion GetExtendedAttributesQuery

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

        public static IServiceCollection AddExtendedAttributeCommandValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            #region AddExtendedAttributeCommandValidator

            var addCommandValidatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(AddExtendedAttributeCommandValidator<,>))
                .ToList();

            foreach (var addCommandValidatorType in addCommandValidatorTypes)
            {
                var addCommandValidatorTypeGenericArguments = addCommandValidatorType.BaseGenericType.GetGenericArguments().ToList();

                var addCommandType = typeof(AddExtendedAttributeCommand<,>).MakeGenericType(addCommandValidatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(addCommandType);
                services.AddScoped(validatorServiceType, addCommandValidatorType.CurrentType);
            }

            #endregion AddExtendedAttributeCommandValidator

            #region UpdateExtendedAttributeCommandValidator

            var updateCommandValidatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(UpdateExtendedAttributeCommandValidator<,>))
                .ToList();

            foreach (var updateCommandValidatorType in updateCommandValidatorTypes)
            {
                var updateCommandValidatorTypeGenericArguments = updateCommandValidatorType.BaseGenericType.GetGenericArguments().ToList();

                var updateCommandType = typeof(UpdateExtendedAttributeCommand<,>).MakeGenericType(updateCommandValidatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(updateCommandType);
                services.AddScoped(validatorServiceType, updateCommandValidatorType.CurrentType);
            }

            #endregion UpdateExtendedAttributeCommandValidator

            #region RemoveExtendedAttributeCommandValidator

            var removeCommandValidatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(RemoveExtendedAttributeCommandValidator<,>))
                .ToList();

            foreach (var removeCommandValidatorType in removeCommandValidatorTypes)
            {
                var removeCommandValidatorTypeGenericArguments = removeCommandValidatorType.BaseGenericType.GetGenericArguments().ToList();

                var removeCommandType = typeof(RemoveExtendedAttributeCommand<,>).MakeGenericType(removeCommandValidatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(removeCommandType);
                services.AddScoped(validatorServiceType, removeCommandValidatorType.CurrentType);
            }

            #endregion RemoveExtendedAttributeCommandValidator

            return services;
        }

        public static IServiceCollection AddPaginatedFilterValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var validatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(PaginatedFilterValidator<,,>))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var validatorTypeGenericArguments = validatorType.BaseGenericType.GetGenericArguments().ToList();
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(validatorTypeGenericArguments.Last());
                services.AddScoped(validatorServiceType, validatorType.CurrentType);
            }

            return services;
        }

        public static IServiceCollection AddExtendedAttributePaginatedFilterValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var validatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType?.IsGenericType == true)
                .Select(t => new
                {
                    BaseGenericType = t.BaseType,
                    CurrentType = t
                })
                .Where(t => t.BaseGenericType?.GetGenericTypeDefinition() == typeof(PaginatedExtendedAttributeFilterValidator<,>))
                .ToList();

            foreach (var validatorType in validatorTypes)
            {
                var validatorTypeGenericArguments = validatorType.BaseGenericType.GetGenericArguments().ToList();

                var filterType = typeof(PaginatedExtendedAttributeFilter<,>).MakeGenericType(validatorTypeGenericArguments.ToArray());
                var validatorServiceType = typeof(IValidator<>).MakeGenericType(filterType);
                services.AddScoped(validatorServiceType, validatorType.CurrentType);
            }

            return services;
        }
    }
}