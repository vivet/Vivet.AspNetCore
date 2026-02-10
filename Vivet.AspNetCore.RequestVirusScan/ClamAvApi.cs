using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using nClam;
using Vivet.AspNetCore.RequestVirusScan.Models;
using Vivet.AspNetCore.RequestVirusScan.Models.Enums;

namespace Vivet.AspNetCore.RequestVirusScan;

/// <summary>
/// Clam Av Api.
/// </summary>
public class ClamAvApi
{
    private readonly ClamClient clamClient;

    /// <summary>
    /// Options.
    /// </summary>
    public IOptionsMonitor<ClamAvOptions> Options { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="clamAvOptions">The <see cref="IOptionsMonitor{ClamAvOptions}"/>.</param>
    public ClamAvApi(IOptionsMonitor<ClamAvOptions> clamAvOptions)
    {
        this.Options = clamAvOptions ?? throw new ArgumentNullException(nameof(clamAvOptions));

        this.clamClient = new ClamClient(this.Options.CurrentValue.Host, this.Options.CurrentValue.Port)
        {
            MaxChunkSize = 131072,
            MaxStreamSize = int.MaxValue
        };
    }

    /// <summary>
    /// Scan.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/>.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
    /// <returns>The <see cref="Result"/>.</returns>
    public virtual async Task<Result> Scan(Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        var clamAvResult = await this.clamClient
            .SendAndScanFileAsync(stream, cancellationToken);

        var infectedFiles = clamAvResult.InfectedFiles?
            .Select(x => new InfectedFile
            {
                FileName = x.FileName,
                VirusName = x.VirusName
            }) ?? new List<InfectedFile>();

        var enumString = clamAvResult.Result.ToString();

        Enum.TryParse<ResultType>(enumString, true, out var resultType);

        return new Result
        {
            Raw = clamAvResult.RawResult,
            Type = resultType,
            InfectedFiles = infectedFiles
        };
    }
}