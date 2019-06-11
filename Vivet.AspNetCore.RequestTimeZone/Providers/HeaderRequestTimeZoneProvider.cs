using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vivet.AspNetCore.RequestTimeZone.Models;

namespace Vivet.AspNetCore.RequestTimeZone.Providers
{
    /// <summary>
    /// Determines the timezone information for a request via the value of the 'tz' header.
    /// </summary>
    public class HeaderRequestTimeZoneProvider : RequestTimeZoneProvider
    {
        /// <summary>
        /// The header key that contains the timezone name.
        /// </summary>
        public virtual string Headerkey { get; set; } = "tz";

        /// <inheritdoc />
        public override Task<ProviderTimeZoneResult> DetermineProviderTimeZoneResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            var value = httpContext.Request
                .Headers[this.Headerkey];

            if (string.IsNullOrEmpty(value))
                return RequestTimeZoneProvider.nullProviderTimeZoneResult;

            var providerTimeZoneResult = new ProviderTimeZoneResult(value);

            return Task.FromResult(providerTimeZoneResult);
        }
    }
}