using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Vivet.AspNetCore.RequestTimeZone.Providers
{
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
        public static readonly string DefaultCookieName = ".AspNetCore.TimeZone";

        /// <summary>
        /// The name of the cookie that contains the user's preferred timezone information.
        /// Defaults to <see cref="DefaultCookieName"/>.
        /// </summary>
        public string CookieName { get; set; } = RequestTimeZoneCookieProvider.DefaultCookieName;

        /// <inheritdoc />
        public override Task<ProviderTimeZoneResult> DetermineProviderTimeZoneResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var value = httpContext.Request
                .Cookies[CookieName];

            if (string.IsNullOrEmpty(value))
                return RequestTimeZoneProvider.nullProviderTimeZoneResult;

            var providerTimeZoneResult = new ProviderTimeZoneResult(value.Replace("tz=", ""));

            return Task.FromResult(providerTimeZoneResult);
        }

        /// <summary>
        /// Creates a string representation of a <see cref="RequestTimeZone"/> for placement in a cookie.
        /// </summary>
        /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>
        /// <returns>The cookie value.</returns>
        public static string MakeCookieValue(RequestTimeZone requestTimeZone)
        {
            if (requestTimeZone == null)
                throw new ArgumentNullException(nameof(requestTimeZone));

            return $"{RequestTimeZoneCookieProvider.PREFIX}{requestTimeZone.TimeZone.Id}";
        }
    }
}