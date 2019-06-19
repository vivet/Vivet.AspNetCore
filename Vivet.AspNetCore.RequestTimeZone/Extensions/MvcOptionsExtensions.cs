using System;
using Microsoft.AspNetCore.Mvc;

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
        /// <returns>The <see cref="MvcOptions"/>.</returns>  
        public static MvcOptions AddDateTimeModelBinderProvider(this MvcOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.ModelBinderProviders
                .Insert(0, new DateTimeModelBinderProvider(new RequestTimeZone(DateTimeInfo.TimeZone.Value)));
            
            return options;
        }
    }
}