using System;
using Vivet.AspNetCore.RequestVirusScan.Models.Enums;

namespace Vivet.AspNetCore.RequestVirusScan.Exceptions;

/// <summary>
/// Virus Scan Exception.
/// </summary>
public class VirusScanException : Exception
{
    /// <summary>
    /// Type.
    /// </summary>
    public ResultType Type { get; set; }

    /// <summary>
    /// Filename.
    /// </summary>
    public string Filename { get; set; }

    /// <summary>
    /// Filename.
    /// </summary>
    public string VirusName { get; set; }

    /// <summary>
    /// Raw.
    /// </summary>
    public string Raw { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">The <see cref="ResultType"/>.</param>
    /// <param name="filename">The filename.</param>
    /// <param name="virusName">The virus name.</param>
    public VirusScanException(ResultType type, string filename, string virusName = null)
        : base($"Virus Scan: Result: {type}, File: {filename}, Virus: {virusName ?? "N/A"}")
    {
        this.Type = type;
        this.Filename = filename ?? throw new ArgumentNullException(nameof(filename));
        this.VirusName = virusName;
    }
}