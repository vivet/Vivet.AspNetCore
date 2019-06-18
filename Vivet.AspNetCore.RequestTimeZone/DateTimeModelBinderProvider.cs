using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vivet.AspNetCore.RequestTimeZone
{
    /// <inheritdoc />
    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        /// <summary>  
        /// Request Time Zone.
        /// </summary>  
        protected virtual Func<RequestTimeZone> RequestTimeZone { get; }

        /// <summary>  
        /// Constructor.
        /// </summary>  
        /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>  
        public DateTimeModelBinderProvider(Func<RequestTimeZone> requestTimeZone)
        {
            this.RequestTimeZone = requestTimeZone ?? throw new ArgumentNullException(nameof(requestTimeZone));
        }

        /// <inheritdoc />
        public virtual IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.UnderlyingOrModelType == typeof(DateTime))
            {
                return new DateTimeModelBinder(this.RequestTimeZone());
            }

            return null; 
        }
    }
}