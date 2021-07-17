using FluentPOS.Shared.Core.Interfaces.Serialization;
using Microsoft.Extensions.Options;
using System.Text.Json;

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
    }
}