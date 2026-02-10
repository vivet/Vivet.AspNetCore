using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vivet.AspNetCore.RequestTimeZone.Providers;

/// <summary>
/// Determines the timezone information for a request via the value of the 'tz' header.
/// </summary>
public class RequestTimeZoneHeaderProvider : RequestTimeZoneProvider
{
    /// <summary>
    /// The header key that contains the timezone name.
    /// </summary>
    public static string Headerkey { get; set; } = "tz";

    /// <inheritdoc />
    public override async Task<ProviderTimeZoneResult?> DetermineProviderTimeZoneResult(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var value = httpContext.Request
            .Headers[RequestTimeZoneHeaderProvider.Headerkey];

        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        var providerTimeZoneResult = new ProviderTimeZoneResult(value.ToString());

        return await Task.FromResult(providerTimeZoneResult);
    }
}