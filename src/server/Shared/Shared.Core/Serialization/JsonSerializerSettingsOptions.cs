using System.Text.Json;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using Newtonsoft.Json;

namespace FluentPOS.Shared.Core.Serialization
{
    public class JsonSerializerSettingsOptions : IJsonSerializerSettingsOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}