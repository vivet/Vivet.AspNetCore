using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vivet.AspNetCore.RequestTimeZone.Middleware;
using Vivet.AspNetCore.RequestTimeZone.Providers.Interfaces;

namespace Vivet.AspNetCore.RequestTimeZone.Providers;

/// <summary>
/// An abstract base class provider for determining the timezone information
/// of an <see cref="HttpRequest"/>.
/// </summary>
public abstract class RequestTimeZoneProvider : IRequestTimeZoneProvider
{
    /// <summary>
    /// The current options for the <see cref="RequestTimeZoneMiddleware"/>.
    /// </summary>
    public RequestTimeZoneOptions Options { get; set; } = null!;

    /// <inheritdoc />
    public abstract Task<ProviderTimeZoneResult?> DetermineProviderTimeZoneResult(HttpContext httpContext);
}