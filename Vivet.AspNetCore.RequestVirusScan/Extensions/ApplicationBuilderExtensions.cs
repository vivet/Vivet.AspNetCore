using System;
using Microsoft.AspNetCore.Builder;
using Vivet.AspNetCore.RequestVirusScan.Middleware;

namespace Vivet.AspNetCore.RequestVirusScan.Extensions;

/// <summary>
/// Application Builder Extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the <see cref="RequestVirusScanMiddleware"/>.
    /// </summary>
    /// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/>.</param>
    /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseRequestVirusScan(this IApplicationBuilder applicationBuilder)
    {
        if (applicationBuilder == null)
            throw new ArgumentNullException(nameof(applicationBuilder));

        return applicationBuilder
            .UseMiddleware<RequestVirusScanMiddleware>();
    }
}