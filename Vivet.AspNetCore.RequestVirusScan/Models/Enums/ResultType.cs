namespace Vivet.AspNetCore.RequestVirusScan.Models.Enums;

/// <summary>
/// Result Type.
/// </summary>
public enum ResultType
{
    /// <summary>
    /// Unknown.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Virus Found.
    /// </summary>
    VirusFound = 1,

    /// <summary>
    /// Clean.
    /// </summary>
    Clean = 2,

    /// <summary>
    /// Error.
    /// </summary>
    Error = 3
}