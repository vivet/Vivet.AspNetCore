using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vivet.AspNetCore.RequestTimeZone.Providers;

/// <summary>
/// Determines the timezone information for a request via the 'tz' query string parameter.
/// </summary>
public class RequestTimeZoneQueryStringProvider : RequestTimeZoneProvider
{
    /// <summary>
    /// The key that contains the timezone name.
    /// </summary>
    public virtual string QueryStringKey { get; set; } = "tz";

    /// <inheritdoc />
    public override async Task<ProviderTimeZoneResult?> DetermineProviderTimeZoneResult(HttpContext httpContext)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        var value = httpContext.Request
            .Query[this.QueryStringKey];

        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        var providerTimeZoneResult = new ProviderTimeZoneResult(value.ToString());

        return await Task.FromResult(providerTimeZoneResult);
    }
}