using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Vivet.AspNetCore.RequestTimeZone.Interfaces;
using Vivet.AspNetCore.RequestTimeZone.Providers;

namespace Vivet.AspNetCore.RequestTimeZone;

/// <inheritdoc />
public class RequestTimeZoneMiddleware : IMiddleware
{
    private readonly ILogger logger;
    private readonly RequestTimeZoneOptions options;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
    /// <param name="options">The <see cref="RequestTimeZoneOptions"/>.</param>
    public RequestTimeZoneMiddleware(ILoggerFactory loggerFactory, RequestTimeZoneOptions options)
    {
        this.logger = loggerFactory?.CreateLogger<RequestTimeZoneMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        if (next == null)
            throw new ArgumentNullException(nameof(next));

        var requestTimeZone = this.options.DefaultRequestTimeZone;
        IRequestTimeZoneProvider winningProvider = null;

        if (this.options.RequestTimeZoneProviders != null)
        {
            foreach (var provider in this.options.RequestTimeZoneProviders)
            {
                var providerTimeZoneResult = await provider
                    .DetermineProviderTimeZoneResult(httpContext);

                if (providerTimeZoneResult == null)
                    continue;

                try
                {
                    var result = new RequestTimeZone(providerTimeZoneResult.TimeZoneName);

                    if (result.TimeZone != null)
                    {
                        requestTimeZone = result;
                        winningProvider = provider;
                        break;
                    }
                }
                catch (InvalidTimeZoneException ex)
                {
                    this.logger.LogWarning(ex, $"Invalid TimeZone Id: {providerTimeZoneResult.TimeZoneName}");
                }
                catch (TimeZoneNotFoundException ex)
                {
                    this.logger.LogWarning(ex, $"TimeZone Not Found: {providerTimeZoneResult.TimeZoneName}");
                }
            }
        }

        httpContext.Features
            .Set<IRequestTimeZoneFeature>(new RequestTimeZoneFeature(requestTimeZone, winningProvider));

        httpContext.Response.Headers[RequestTimeZoneHeaderProvider.Headerkey] = requestTimeZone.TimeZone.Id;

        DateTimeInfo.TimeZone.Value = requestTimeZone.TimeZone;

        await next(httpContext);
    }
}