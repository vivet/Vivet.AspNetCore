using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions
{
    /// <summary>  
    /// Mvc Json Options Extensions.
    /// </summary>  
    public static class MvcJsonOptionsExtensions
    {
        /// <summary>  
        /// Inserts <see cref="DateTimeConverter"/> as serialization converter.  
        /// </summary>  
        /// <param name="options">The <see cref="MvcJsonOptions"/>.</param>  
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>  
        /// <returns>The <see cref="MvcJsonOptions"/>.</returns>  
        public static MvcJsonOptions AddDateTimeConverter(this MvcJsonOptions options, IServiceCollection serviceCollection)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var requestTimeZone = serviceCollection
                .BuildServiceProvider()
                .GetService<RequestTimeZone>(); 

            options.SerializerSettings.Converters
                .Add(new DateTimeConverter(() => requestTimeZone));

            return options;
        }
    }
}