namespace Vivet.AspNetCore.RequestVirusScan;

/// <summary>
/// Clam Av Options.
/// </summary>
public class ClamAvOptions
{
    internal static string SectionName => "ClamAv";

    /// <summary>
    /// Host.
    /// </summary>
    public virtual string Host { get; set; }

    /// <summary>
    /// Port.
    /// </summary>
    public virtual int Port { get; set; }

    /// <summary>
    /// Use Health Check.
    /// </summary>
    public virtual bool UseHealthCheck { get; set; } = true;
}