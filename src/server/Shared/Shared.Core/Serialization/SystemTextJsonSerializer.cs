// --------------------------------------------------------------------------------------------------
// <copyright file="SystemTextJsonSerializer.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Text.Json;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using Microsoft.Extensions.Options;

namespace FluentPOS.Shared.Core.Serialization
{
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerOptions _options;

        public SystemTextJsonSerializer(IOptions<JsonSerializerSettingsOptions> options)
        {
            _options = options.Value.JsonSerializerOptions;
        }

        public T Deserialize<T>(string data, IJsonSerializerSettingsOptions options = null)
            => JsonSerializer.Deserialize<T>(data, options?.JsonSerializerOptions ?? _options);

        public string Serialize<T>(T data, IJsonSerializerSettingsOptions options = null)
            => JsonSerializer.Serialize(data, options?.JsonSerializerOptions ?? _options);

        public string Serialize<T>(T data, Type type, IJsonSerializerSettingsOptions options = null)
            => JsonSerializer.Serialize(data, type, options?.JsonSerializerOptions ?? _options);
    }
}