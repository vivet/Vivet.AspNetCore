using System;
using System.Threading;

namespace Vivet.AspNetCore.RequestTimeZone;

/// <summary>
/// Date Time Info.
/// </summary>
public static class DateTimeInfo
{
    /// <summary>
    /// Time Zone.
    /// </summary>
    public static AsyncLocal<TimeZoneInfo> TimeZone { get; set; } = new(_ => TimeZoneInfo.FindSystemTimeZoneById("UTC"));

    /// <summary>
    /// Returns the local date time, based on the <see cref="TimeZone"/>.
    /// The <see cref="DateTimeOffset.UtcNow"/> converted by TimeZoneId.
    /// </summary>
    public static DateTimeOffset Now => DateTimeInfo.TimeZone.Value != null
        ? TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTimeOffset.UtcNow, DateTimeInfo.TimeZone.Value.Id)
        : DateTimeOffset.UtcNow;

    /// <summary>
    /// Returns the <see cref="DateTimeOffset.UtcNow"/>.
    /// </summary>
    public static DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}