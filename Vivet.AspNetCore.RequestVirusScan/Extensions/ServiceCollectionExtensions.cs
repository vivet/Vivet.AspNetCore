﻿using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRequestVirusScan(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services
            .AddConfigOptions<ClamAvOptions>(ClamAvOptions.SectionName, out var options);

        services
            .AddSingleton<ClamAvApi>()
            .AddSingleton<RequestVirusScanMiddleware>();

        if (options.UseHealthCheck)
        {
            services
                .AddHealthChecks()
                .AddTcpHealthCheck(x => x.AddHost(options.Host, options.Port), "clamav");
        }

        return services;
    }

    /// <summary>
    /// Add Request Virus Scan.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="optionsAction">The <see cref="Action{ClamAvOptions}"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRequestVirusScan(this IServiceCollection services, Action<ClamAvOptions> optionsAction)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services
            .AddSingleton<ClamAvApi>();

        var options = new ClamAvOptions();
        optionsAction(options);

        services
            .AddSingleton(_ => options)
            .AddSingleton<RequestVirusScanMiddleware>();

        if (options.UseHealthCheck)
        {
            services
                .AddHealthChecks()
                .AddTcpHealthCheck(x => x.AddHost(options.Host, options.Port), "clamav");
        }

        return services;
    }

    /// <summary>
    /// Adds a appOptions <see cref="IConfigurationSection"/> as <see cref="IOptions{TOptions}"/> to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TOption">The option implementation type.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="name">The name of the section.</param>
    /// <param name="options">The options configured and loaded.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    private static IServiceCollection AddConfigOptions<TOption>(this IServiceCollection services, string name, out TOption options)
        where TOption : class, new()
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (name == null)
            throw new ArgumentNullException(nameof(name));

        var provider = services
            .BuildServiceProvider();

        var configuration = provider
            .GetRequiredService<IConfiguration>();

        var section = configuration
            .GetSection(name);

        options = section.Get<TOption>() ?? new TOption();

        services
            .AddSingleton(options)
            .Configure<TOption>(section);

        return services;
    }
}