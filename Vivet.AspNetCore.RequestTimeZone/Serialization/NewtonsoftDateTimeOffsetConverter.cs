using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Vivet.AspNetCore.RequestTimeZone.Serialization;

/// <inheritdoc />
public class NewtonsoftDateTimeOffsetConverter : DateTimeConverterBase
{
    /// <summary>
    /// Request Time Zone.
    /// </summary>
    protected virtual Func<RequestTimeZone> RequestTimeZone { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>
    public NewtonsoftDateTimeOffsetConverter(Func<RequestTimeZone> requestTimeZone)
    {
        this.RequestTimeZone = requestTimeZone ?? throw new ArgumentNullException(nameof(requestTimeZone));
    }

    /// <inheritdoc />
    public override bool CanConvert(Type objectType)
    {
        return
            objectType == typeof(DateTimeOffset) ||
            objectType == typeof(DateTimeOffset?);
    }

    /// <inheritdoc />
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer? serializer)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var value = reader.Value;

        if (value == null)
        {
            return null;
        }

        DateTimeOffset.TryParse(value.ToString(), out var parsedDateTime);

        return parsedDateTime;
    }

    /// <inheritdoc />
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer? serializer)
    {
        ArgumentNullException.ThrowIfNull(writer);

        if (value == null)
        {
            return;
        }

        DateTimeOffset.TryParse(value.ToString(), out var parsedDateTime);

        var timeZone = this.RequestTimeZone().TimeZone;
        var convertTime = TimeZoneInfo.ConvertTime(parsedDateTime, timeZone);

        writer
            .WriteValue(convertTime);

        writer
            .Flush();
    }
}