using System;
using Microsoft.Extensions.DependencyInjection;
using Vivet.AspNetCore.RequestVirusScan.Middleware;

namespace Vivet.AspNetCore.RequestVirusScan.Extensions;

/// <summary>
/// Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Request Virus Scan.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="optionsAction">The <see cref="Action{ClamAvOptions}"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRequestVirusScan(this IServiceCollection services, Action<ClamAvOptions> optionsAction)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(optionsAction);

        var options = new ClamAvOptions();

        optionsAction
            .Invoke(options);

        services
            .Configure(optionsAction);

        services
            .AddScoped<ClamAvApi>()
            .AddScoped<RequestVirusScanMiddleware>();

        if (options.UseHealthCheck)
        {
            services
                .AddHealthChecks()
                .AddTcpHealthCheck(x => x
                    .AddHost(options.Host, options.Port), "clamav", options.HealthCheckFailureStatus);
        }

        return services;
    }
}