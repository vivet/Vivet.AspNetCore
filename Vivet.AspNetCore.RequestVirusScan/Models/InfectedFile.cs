namespace Vivet.AspNetCore.RequestVirusScan.Models;

/// <summary>
/// Infected File.
/// </summary>
public record InfectedFile
{
    /// <summary>
    /// File Name.
    /// </summary>
    public virtual string FileName { get; set; }

    /// <summary>
    /// Virus Name
    /// </summary>
    public virtual string VirusName { get; set; }
}