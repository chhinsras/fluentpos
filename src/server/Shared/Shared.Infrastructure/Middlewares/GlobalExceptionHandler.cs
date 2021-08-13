// --------------------------------------------------------------------------------------------------
// <copyright file="GlobalExceptionHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Exceptions;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Serialization;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Core.Wrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace FluentPOS.Shared.Infrastructure.Middlewares
{
    internal class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly SerializationSettings _serializationSettings;
        private readonly IJsonSerializer _jsonSerializer;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IOptions<SerializationSettings> serializationSettings,
            IJsonSerializer jsonSerializer)
        {
            _logger = logger;
            _serializationSettings = serializationSettings.Value;
            _jsonSerializer = jsonSerializer;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                var responseModel = await ErrorResult<string>.ReturnErrorAsync(exception.Message);
                responseModel.Source = exception.Source;
                responseModel.Exception = exception.Message;
                _logger.LogError(exception.Message);
                switch (exception)
                {
                    case CustomException e:
                        response.StatusCode = responseModel.ErrorCode = (int)e.StatusCode;
                        responseModel.Messages = e.ErrorMessages;
                        break;

                    case KeyNotFoundException:
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = responseModel.ErrorCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                string result = string.Empty;
                if (_serializationSettings.UseNewtonsoftJson)
                {
                    result = _jsonSerializer.Serialize(responseModel, new JsonSerializerSettingsOptions
                    {
                        JsonSerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                    });
                }
                else if (_serializationSettings.UseSystemTextJson)
                {
                    result = _jsonSerializer.Serialize(responseModel, new JsonSerializerSettingsOptions
                    {
                        JsonSerializerOptions = { DictionaryKeyPolicy = JsonNamingPolicy.CamelCase, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                    });
                }

                await response.WriteAsync(result);
            }
        }
    }
}