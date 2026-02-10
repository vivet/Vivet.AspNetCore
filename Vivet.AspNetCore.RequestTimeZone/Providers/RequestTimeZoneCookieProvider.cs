using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Vivet.AspNetCore.RequestTimeZone.Providers;

/// <summary>
/// Determines the timezone information for a request via the value of a cookie.
/// </summary>
public class RequestTimeZoneCookieProvider : RequestTimeZoneProvider
{
    private const string PREFIX = "tz=";

    /// <summary>
    /// Represent the default cookie name used to track the user's preferred timezone information,
    /// which is ".AspNetCore.TimeZone".
    /// </summary>
    public static readonly string defaultCookieName = ".AspNetCore.TimeZone";

    /// <summary>
    /// The name of the cookie that contains the user's preferred timezone information.
    /// Defaults to <see cref="defaultCookieName"/>.
    /// </summary>
    public string CookieName { get; set; } = RequestTimeZoneCookieProvider.defaultCookieName;

    /// <inheritdoc />
    public override async Task<ProviderTimeZoneResult?> DetermineProviderTimeZoneResult(HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        var value = httpContext.Request
            .Cookies[CookieName];

        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        var providerTimeZoneResult = new ProviderTimeZoneResult(value.Replace(RequestTimeZoneCookieProvider.PREFIX, ""));

        return await Task.FromResult(providerTimeZoneResult);
    }

    /// <summary>
    /// Creates a string representation of a <see cref="RequestTimeZone"/> for placement in a cookie.
    /// </summary>
    /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>
    /// <returns>The cookie value.</returns>
    public static string MakeCookieValue(RequestTimeZone requestTimeZone)
    {
        ArgumentNullException.ThrowIfNull(requestTimeZone);

        return $"{RequestTimeZoneCookieProvider.PREFIX}{requestTimeZone.TimeZone.Id}";
    }
}