using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vivet.AspNetCore.RequestTimeZone
{
    /// <inheritdoc />
    public class DateTimeModelBinder : IModelBinder
    {
        /// <summary>  
        /// Request Time Zone.
        /// </summary>  
        protected virtual Func<RequestTimeZone> RequestTimeZone { get; }

        /// <summary>  
        /// Constructor.
        /// </summary>  
        /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>  
        public DateTimeModelBinder(Func<RequestTimeZone> requestTimeZone)
        {
            this.RequestTimeZone = requestTimeZone ?? throw new ArgumentNullException(nameof(requestTimeZone));
        }
        
        /// <inheritdoc />
        public virtual Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var value = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);
            
            if (string.IsNullOrEmpty(value.FirstValue))
                return null;

            var success = DateTimeOffset.TryParse(value.FirstValue, null, DateTimeStyles.AdjustToUniversal, out var datetime);

            if (success)
            {
                var timeZone = this.RequestTimeZone().TimeZone;
                var dateTimeUtc = TimeZoneInfo.ConvertTime(datetime, timeZone).ToUniversalTime();
                bindingContext.Result = ModelBindingResult.Success(dateTimeUtc);
            }
            else
            {
                var accessor = bindingContext.ModelMetadata.ModelBindingMessageProvider
                    .AttemptedValueIsInvalidAccessor(value.ToString(), nameof(DateTimeOffset));
                
                bindingContext.ModelState
                    .TryAddModelError(bindingContext.ModelName, accessor);
            }

            return Task.CompletedTask;
        }
    }
}