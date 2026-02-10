using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Vivet.AspNetCore.RequestTimeZone.Features;
using Vivet.AspNetCore.RequestTimeZone.Features.Interfaces;
using Vivet.AspNetCore.RequestTimeZone.Providers;
using Vivet.AspNetCore.RequestTimeZone.Providers.Interfaces;

namespace Vivet.AspNetCore.RequestTimeZone.Middleware;

/// <inheritdoc />
public class RequestTimeZoneMiddleware : IMiddleware
{
    private readonly ILogger logger;
    private readonly IOptionsMonitor<RequestTimeZoneOptions> options;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="loggerFactory">The <see cref="ILoggerFactory"/>.</param>
    /// <param name="options">The <see cref="RequestTimeZoneOptions"/>.</param>
    public RequestTimeZoneMiddleware(ILoggerFactory loggerFactory, IOptionsMonitor<RequestTimeZoneOptions> options)
    {
        this.logger = loggerFactory.CreateLogger<RequestTimeZoneMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        if (next == null)
            throw new ArgumentNullException(nameof(next));

        var requestTimeZone = new RequestTimeZone(this.options.CurrentValue.DefaultTimeZone);

        IRequestTimeZoneProvider? winningProvider = null;

        foreach (var provider in this.options.CurrentValue.RequestTimeZoneProviders)
        {
            var providerTimeZoneResult = await provider
                .DetermineProviderTimeZoneResult(httpContext);

            if (providerTimeZoneResult == null)
            {
                continue;
            }

            try
            {
                var result = new RequestTimeZone(providerTimeZoneResult.TimeZoneName);

                requestTimeZone = result;
                winningProvider = provider;

                break;
            }
            catch (InvalidTimeZoneException ex)
            {
                this.logger
                    .LogWarning(ex, $"Invalid TimeZone Id: {providerTimeZoneResult.TimeZoneName}");
            }
            catch (TimeZoneNotFoundException ex)
            {
                this.logger
                    .LogWarning(ex, $"TimeZone Not Found: {providerTimeZoneResult.TimeZoneName}");
            }
        }

        httpContext.Features
            .Set<IRequestTimeZoneFeature>(new RequestTimeZoneFeature(requestTimeZone, winningProvider));

        DateTimeInfo.TimeZone.Value = requestTimeZone.TimeZone;

        httpContext.Response.Headers[RequestTimeZoneHeaderProvider.Headerkey] = requestTimeZone.TimeZone.Id;

        await next(httpContext);
    }
}