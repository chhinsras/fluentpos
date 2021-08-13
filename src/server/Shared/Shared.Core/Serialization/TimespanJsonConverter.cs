// --------------------------------------------------------------------------------------------------
// <copyright file="TimespanJsonConverter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace FluentPOS.Shared.Core.Serialization
{
    /// <summary>
    /// The new Json.NET doesn't support Timespan at this time.
    /// https://github.com/dotnet/corefx/issues/38641.
    /// </summary>
    public class TimespanJsonConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Format: Days.Hours:Minutes:Seconds:Milliseconds.
        /// </summary>
        public const string TimeSpanFormatString = @"d\.hh\:mm\:ss\:FFF";

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string s = reader.GetString();
            if (string.IsNullOrWhiteSpace(s))
            {
                return TimeSpan.Zero;
            }

            if (!TimeSpan.TryParseExact(s, TimeSpanFormatString, null, out var parsedTimeSpan))
            {
                throw new FormatException($"Input timespan is not in an expected format : expected {Regex.Unescape(TimeSpanFormatString)}. Please retrieve this key as a string and parse manually.");
            }

            return parsedTimeSpan;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            string timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
            writer.WriteStringValue(timespanFormatted);
        }
    }
}