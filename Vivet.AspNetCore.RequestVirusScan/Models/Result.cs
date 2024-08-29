using System.Collections.Generic;
using Vivet.AspNetCore.RequestVirusScan.Models.Enums;

namespace Vivet.AspNetCore.RequestVirusScan.Models;

/// <summary>
/// Result.
/// </summary>
public class Result
{
    /// <summary>
    /// Raw.
    /// </summary>
    public virtual string Raw { get; set; }

    /// <summary>
    /// Type.
    /// </summary>
    public virtual ResultType Type { get; set; }

    /// <summary>
    /// Infected Files.
    /// </summary>
    public virtual IEnumerable<InfectedFile> InfectedFiles { get; set; }
}