using System;
using Microsoft.AspNetCore.Mvc;

namespace Vivet.AspNetCore.RequestTimeZone.Extensions
{
    /// <summary>  
    /// Mvc Newtonsoft Json Options Extensions.
    /// </summary>  
    public static class MvcNewtonsoftJsonOptionsExtensions
    {
        /// <summary>  
        /// Inserts <see cref="DateTimeConverter"/> as serialization converter.  
        /// </summary>  
        /// <param name="options">The <see cref="MvcNewtonsoftJsonOptions"/>.</param>  
        /// <returns>The <see cref="MvcNewtonsoftJsonOptions"/>.</returns>  
        public static MvcNewtonsoftJsonOptions AddDateTimeConverter(this MvcNewtonsoftJsonOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.SerializerSettings.Converters
                .Add(new DateTimeConverter(() => new RequestTimeZone(DateTimeInfo.TimeZone.Value)));

            return options;
        }
    }
}