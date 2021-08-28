// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.IntegrationServices.Application;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Controllers;
using FluentPOS.Shared.Infrastructure.EventLogging;
using FluentPOS.Shared.Infrastructure.Interceptors;
using FluentPOS.Shared.Infrastructure.Middlewares;
using FluentPOS.Shared.Infrastructure.Persistence;
using FluentPOS.Shared.Infrastructure.Services;
using FluentPOS.Shared.Infrastructure.Swagger.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

[assembly: InternalsVisibleTo("Bootstrapper")]

namespace FluentPOS.Shared.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExtendedAttributeDbContextsFromAssembly(this IServiceCollection services, Type implementationType, Assembly assembly)
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
                var serviceType = typeof(IExtendedAttributeDbContext<,,>).MakeGenericType(extendedAttributeTypeGenericArguments[0], extendedAttributeTypeGenericArguments[1], extendedAttributeType.CurrentType);
                services.AddScoped(serviceType, provider => provider.GetService(implementationType));
            }

            return services;
        }

        internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPersistenceSettings(config);
            services
                .AddDatabaseContext<ApplicationDbContext>()
                .AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IEventLogger, EventLogger>();
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                })
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((value, propertyName) =>
                        throw new CustomException($"{propertyName}: value '{value}' is invalid.", statusCode: HttpStatusCode.BadRequest));
                });
            services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
            services.AddApplicationLayer(config);
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddHangfireServer();
            services.AddSingleton<GlobalExceptionHandler>();
            services.AddSwaggerDocumentation();
            services.AddCorsPolicy();
            services.AddApplicationSettings(config);
            return services;
        }

        private static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<IMailService, SmtpMailService>();
            services.AddTransient<ISmsService, TwilioSmsService>();
            services.AddScoped<IJobService, HangfireService>();
            services.Configure<MailSettings>(config.GetSection(nameof(MailSettings)));
            services.Configure<SmsSettings>(config.GetSection(nameof(SmsSettings)));
            services.AddTransient<IEventLogService, EventLogService>();
            services.AddTransient<IEntityReferenceService, EntityReferenceService>();
            return services;
        }

        private static IServiceCollection AddPersistenceSettings(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Configure<PersistenceSettings>(config.GetSection(nameof(PersistenceSettings)));
        }

        private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.IsDynamic)
                    {
                        string xmlFile = $"{assembly.GetName().Name}.xml";
                        string xmlPath = Path.Combine(baseDirectory, xmlFile);
                        if (File.Exists(xmlPath))
                        {
                            options.IncludeXmlComments(xmlPath);
                        }
                    }
                }

                options.AddSwaggerDocs();

                options.OperationFilter<RemoveVersionFromParameterFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                options.OperationFilter<SwaggerExcludeFilter>();
                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                    {
                        return false;
                    }

                    var versions = methodInfo
                        .DeclaringType?
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToList();

                    return versions?.Any(v => $"v{v}" == version) == true
                           && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
                options.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Nullable = true,
                    Pattern = @"^([0-9]{1}|(?:0[0-9]|1[0-9]|2[0-3])+):([0-5]?[0-9])(?::([0-5]?[0-9])(?:.(\d{1,9}))?)?$",
                    Example = new OpenApiString("02:00:00")
                });
            });
        }

        private static void AddSwaggerDocs(this SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "FluentPOS.API v1",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "FluentPOS.API v2",
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        }

        private static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
        }

        private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            var corsSettings = services.GetOptions<CorsSettings>(nameof(CorsSettings));
            return services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(corsSettings.Url);
                });
            });
        }
    }
}