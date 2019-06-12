﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions
{
    /// <summary>
    /// Service Collection Extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services to the <see cref="IServiceCollection"/>, required for request timezone support
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="defaultTimeZone">The name of the time zone that should be used as default.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRequestTimeZone(this IServiceCollection services, string defaultTimeZone)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .AddRequestTimeZone(x =>
                {
                    x.DefaultRequestTimeZone = new Models.RequestTimeZone(defaultTimeZone);
                });

            return services;
        }

        /// <summary>
        /// Adds services to the <see cref="IServiceCollection"/>, required for request timezone support
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="optionsAction">The action returning <see cref="RequestTimeZoneOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRequestTimeZone(this IServiceCollection services, Action<RequestTimeZoneOptions> optionsAction)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var options = new RequestTimeZoneOptions();
            optionsAction.Invoke(options);

            services
                .AddSingleton(x => options)
                .AddSingleton<RequestTimeZoneMiddleware>();

            return services;
        }
    }
}