using System;
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
                .AddScoped<RequestTimeZone>()
                .AddSingleton<RequestTimeZoneMiddleware>();

            if (options.EnableRequestToUtc)
            {
                services
                    .AddMvc(x =>
                    {
                        x.AddDateTimeModelBinderProvider(services); 
                    });
            }

            if (options.EnableRequestToUtc)
            {
                services
                    .AddMvc()
                    .AddJsonOptions(x =>
                    {
                        x.AddDateTimeConverter(services);
                    });
            }
            
            return services;
        }
    }
}