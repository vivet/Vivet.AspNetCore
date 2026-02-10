using Microsoft.AspNetCore.Mvc;
using System;
using Vivet.AspNetCore.RequestTimeZone.Serialization;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions;

/// <summary>
/// Mvc Json Options Extensions.
/// </summary>
public static class MvcJsonOptionsExtensions
{
    /// <summary>
    /// Inserts <see cref="MicrosoftDateTimeOffsetConverter"/> as serialization converter.
    /// </summary>
    /// <param name="options">The <see cref="JsonOptions"/>.</param>
    /// <returns>The <see cref="JsonOptions"/>.</returns>
    public static JsonOptions AddDateTimeConverter(this JsonOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.JsonSerializerOptions.Converters
            .Add(new MicrosoftDateTimeOffsetConverter(() => new RequestTimeZone(DateTimeInfo.TimeZone.Value!)));

        return options;
    }
}