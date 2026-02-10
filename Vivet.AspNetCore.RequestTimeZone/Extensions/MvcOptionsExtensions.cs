using Microsoft.AspNetCore.Mvc;
using System;
using Vivet.AspNetCore.RequestTimeZone.ModelBinders;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions;

/// <summary>
/// Mvc Options Extensions.
/// </summary>
public static class MvcOptionsExtensions
{
    /// <summary>
    /// Inserts the <see cref="DateTimeOffsetModelBinderProvider"/> as model binder provider.
    /// </summary>
    /// <param name="options">The <see cref="MvcOptions"/>.</param>
    /// <returns>The <see cref="MvcOptions"/>.</returns>
    public static MvcOptions AddDateTimeModelBinderProvider(this MvcOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.ModelBinderProviders
            .Insert(0, new DateTimeOffsetModelBinderProvider(() => new RequestTimeZone(DateTimeInfo.TimeZone.Value!)));

        return options;
    }
}