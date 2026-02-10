using Microsoft.AspNetCore.Mvc;
using System;
using Vivet.AspNetCore.RequestTimeZone.Serialization;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions;

/// <summary>
/// Mvc Newtonsoft Json Options Extensions.
/// </summary>
public static class MvcNewtonsoftJsonOptionsExtensions
{
    /// <summary>
    /// Inserts <see cref="NewtonsoftDateTimeOffsetConverter"/> as serialization converter.
    /// </summary>
    /// <param name="options">The <see cref="MvcNewtonsoftJsonOptions"/>.</param>
    /// <returns>The <see cref="MvcNewtonsoftJsonOptions"/>.</returns>
    public static MvcNewtonsoftJsonOptions AddDateTimeConverter(this MvcNewtonsoftJsonOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.SerializerSettings.Converters
            .Add(new NewtonsoftDateTimeOffsetConverter(() => new RequestTimeZone(DateTimeInfo.TimeZone.Value!)));

        return options;
    }
}