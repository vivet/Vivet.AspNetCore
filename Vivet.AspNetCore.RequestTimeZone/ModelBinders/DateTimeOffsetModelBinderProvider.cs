using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vivet.AspNetCore.RequestTimeZone.ModelBinders;

/// <inheritdoc />
public class DateTimeOffsetModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Request Time Zone.
    /// </summary>
    protected virtual Func<RequestTimeZone> RequestTimeZone { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="requestTimeZone">The <see cref="RequestTimeZone"/>.</param>
    public DateTimeOffsetModelBinderProvider(Func<RequestTimeZone> requestTimeZone)
    {
        this.RequestTimeZone = requestTimeZone ?? throw new ArgumentNullException(nameof(requestTimeZone));
    }

    /// <inheritdoc />
    public virtual IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        return context.Metadata.UnderlyingOrModelType == typeof(DateTimeOffset)
            ? new DateTimeOffsetModelBinder(this.RequestTimeZone)
            : null;
    }
}