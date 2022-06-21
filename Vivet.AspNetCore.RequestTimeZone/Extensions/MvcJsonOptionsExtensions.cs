using System;
using Microsoft.AspNetCore.Mvc;
using Vivet.AspNetCore.RequestTimeZone.Serialization;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions;

/// <summary>
/// Mvc Json Options Extensions.
/// </summary>
public static class MvcJsonOptionsExtensions
{
    /// <summary>
    /// Inserts <see cref="MicrosoftDateTimeConverter"/> as serialization converter.
    /// </summary>
    /// <param name="options">The <see cref="JsonOptions"/>.</param>
    /// <returns>The <see cref="JsonOptions"/>.</returns>
    public static JsonOptions AddDateTimeConverter(this JsonOptions options)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        options.JsonSerializerOptions.Converters
            .Add(new MicrosoftDateTimeConverter(() => new RequestTimeZone(DateTimeInfo.TimeZone.Value)));

        return options;
    }
}