using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vivet.AspNetCore.RequestTimeZone;

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

        var valueProviderResult = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName);

        if (string.IsNullOrEmpty(valueProviderResult.FirstValue))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
        }
        else
        {
            var success = DateTimeOffset.TryParse(valueProviderResult.FirstValue, null, DateTimeStyles.AdjustToUniversal, out var parsedDateTime);

            if (success)
            {
                var timeZone = this.RequestTimeZone().TimeZone;
                var dateTimeUtc = TimeZoneInfo.ConvertTime(parsedDateTime, timeZone).ToUniversalTime();

                bindingContext.Result = ModelBindingResult.Success(dateTimeUtc);
            }
            else
            {
                var invalidAccessor = bindingContext.ModelMetadata
                    .ModelBindingMessageProvider.AttemptedValueIsInvalidAccessor(valueProviderResult.ToString(), nameof(DateTimeOffset));

                bindingContext.ModelState
                    .TryAddModelError(bindingContext.ModelName, invalidAccessor);
            }
        }

        return Task.CompletedTask;
    }
}