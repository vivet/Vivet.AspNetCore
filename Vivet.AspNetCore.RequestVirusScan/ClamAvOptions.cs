using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Vivet.AspNetCore.RequestVirusScan;

/// <summary>
/// Clam Av Options.
/// </summary>
public class ClamAvOptions
{
    /// <summary>
    /// Host.
    /// </summary>
    public virtual string Host { get; set; } = null!;

    /// <summary>
    /// Port.
    /// </summary>
    public virtual int Port { get; set; } = 3310;

    /// <summary>
    /// Use Health Check.
    /// </summary>
    public virtual bool UseHealthCheck { get; set; } = true;

    /// <summary>
    /// Use Health Check Failure Status.
    /// </summary>
    public virtual HealthStatus HealthCheckFailureStatus { get; set; } = HealthStatus.Unhealthy;
}