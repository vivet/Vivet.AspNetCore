using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vivet.AspNetCore.RequestTimeZone.Providers;

namespace Vivet.AspNetCore.RequestTimeZone.Interfaces;

/// <summary>
/// Request TimeZone Provider interface.
/// </summary>
public interface IRequestTimeZoneProvider
{
    /// <summary>
    /// Determines the provider timezone result from the <see cref="HttpContext"/>.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
    /// <returns>The <see cref="ProviderTimeZoneResult"/>.</returns>
    Task<ProviderTimeZoneResult> DetermineProviderTimeZoneResult(HttpContext httpContext);
}