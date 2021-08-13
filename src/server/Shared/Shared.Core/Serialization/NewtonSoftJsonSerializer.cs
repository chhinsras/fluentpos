// --------------------------------------------------------------------------------------------------
// <copyright file="NewtonSoftJsonSerializer.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FluentPOS.Shared.Core.Serialization
{
    public class NewtonSoftJsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public NewtonSoftJsonSerializer(IOptions<JsonSerializerSettingsOptions> settings)
        {
            _settings = settings.Value.JsonSerializerSettings;
        }

        public T Deserialize<T>(string text, IJsonSerializerSettingsOptions settings = null)
            => JsonConvert.DeserializeObject<T>(text, settings?.JsonSerializerSettings ?? _settings);

        public string Serialize<T>(T obj, IJsonSerializerSettingsOptions settings = null)
            => JsonConvert.SerializeObject(obj, settings?.JsonSerializerSettings ?? _settings);

        public string Serialize<T>(T obj, Type type, IJsonSerializerSettingsOptions settings = null)
            => JsonConvert.SerializeObject(obj, type, settings?.JsonSerializerSettings ?? _settings);
    }
}