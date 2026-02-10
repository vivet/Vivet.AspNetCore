using Microsoft.AspNetCore.Http;
using System;
using Vivet.AspNetCore.RequestTimeZone.Features.Interfaces;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions;

/// <summary>
/// Http Context Extensions.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Get the <see cref="TimeZoneInfo"/>.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
    /// <returns>The token.</returns>
    public static TimeZoneInfo? GetUserTimeZone(this HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        return httpContext.Features
            .Get<IRequestTimeZoneFeature>()?
            .RequestTimeZone
            .TimeZone;
    }
}