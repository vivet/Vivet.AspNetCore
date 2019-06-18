using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions
{
    /// <summary>  
    /// Mvc Options Extensions.
    /// </summary>  
    public static class MvcOptionsExtensions
    {
        /// <summary>  
        /// Inserts the <see cref="DateTimeModelBinderProvider"/> as model binder provider.  
        /// </summary>  
        /// <param name="options">The <see cref="MvcOptions"/>.</param>  
        /// <param name="serviceCollection">The <see cref="IServiceCollection"/>.</param>  
        /// <returns>The <see cref="MvcOptions"/>.</returns>  
        public static MvcOptions AddDateTimeModelBinderProvider(this MvcOptions options, IServiceCollection serviceCollection)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var requestTimeZone = serviceCollection
                .BuildServiceProvider()
                .GetService<RequestTimeZone>(); 

            options.ModelBinderProviders
                .Insert(0, new DateTimeModelBinderProvider(() => requestTimeZone));
            
            return options;
        }
    }
}