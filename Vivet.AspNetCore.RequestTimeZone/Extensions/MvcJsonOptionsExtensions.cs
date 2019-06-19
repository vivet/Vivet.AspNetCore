using System;
using Microsoft.AspNetCore.Mvc;

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
        /// <returns>The <see cref="MvcJsonOptions"/>.</returns>  
        public static MvcJsonOptions AddDateTimeConverter(this MvcJsonOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.SerializerSettings.Converters
                .Add(new DateTimeConverter(new RequestTimeZone(DateTimeInfo.TimeZone.Value)));

            return options;
        }
    }
}