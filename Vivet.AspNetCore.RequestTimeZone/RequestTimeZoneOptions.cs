using System.Collections.Generic;
using Vivet.AspNetCore.RequestTimeZone.Enums;
using Vivet.AspNetCore.RequestTimeZone.Middleware;
using Vivet.AspNetCore.RequestTimeZone.Providers;
using Vivet.AspNetCore.RequestTimeZone.Providers.Interfaces;

namespace Vivet.AspNetCore.RequestTimeZone;

/// <summary>
/// Specifies options for the <see cref="RequestTimeZoneMiddleware"/>.
/// </summary>
public class RequestTimeZoneOptions
{
    private readonly RequestTimeZone defaultRequestTimeZone = new("UTC");

    /// <summary>
    /// Gets or sets the default timezone to use for requests.
    /// </summary>
    public virtual RequestTimeZone DefaultRequestTimeZone => string.IsNullOrEmpty(this.Id)
        ? this.defaultRequestTimeZone
        : new RequestTimeZone(this.Id);

    /// <summary>
    /// Id.
    /// The timezone identifier.
    /// </summary>
    public virtual string Id { get; set; }

    /// <summary>
    /// Enable Request To Utc.
    /// Enables conversion of date time values in request to UTC.
    /// </summary>
    public virtual bool EnableRequestToUtc { get; set; }

    /// <summary>
    /// Enable Response To Local.
    /// Enables conversion of date time values in response to local.
    /// </summary>
    public virtual bool EnableResponseToLocal { get; set; }

    /// <summary>
    /// Json Serializer Type.
    /// The serializer used with the application.
    /// </summary>
    public virtual JsonSerializerType JsonSerializerType { get; set; } = JsonSerializerType.Newtonsoft;

    /// <summary>
    /// An ordered list of providers used to determine a request's timezone information.
    /// The first provider that returns a non-null result for a given request will be used.
    /// Defaults to the following:
    /// <list type="number">
    ///     <item><description><see cref="RequestTimeZoneQueryStringProvider"/></description></item>
    ///     <item><description><see cref="RequestTimeZoneHeaderProvider"/></description></item>
    ///     <item><description><see cref="RequestTimeZoneCookieProvider"/></description></item>
    /// </list>
    /// </summary>
    public IList<IRequestTimeZoneProvider> RequestTimeZoneProviders { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public RequestTimeZoneOptions()
    {
        this.RequestTimeZoneProviders = new List<IRequestTimeZoneProvider>
        {
            new RequestTimeZoneQueryStringProvider { Options = this },
            new RequestTimeZoneCookieProvider { Options = this },
            new RequestTimeZoneHeaderProvider { Options = this }
        };
    }
}