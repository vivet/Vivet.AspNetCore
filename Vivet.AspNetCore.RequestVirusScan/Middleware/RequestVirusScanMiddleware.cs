using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vivet.AspNetCore.RequestVirusScan.Exceptions;
using Vivet.AspNetCore.RequestVirusScan.Models.Enums;

namespace Vivet.AspNetCore.RequestVirusScan.Middleware;

/// <inheritdoc />
public class RequestVirusScanMiddleware : IMiddleware
{
    private readonly ClamAvApi clamAvClient;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="clamAvClient">The <see cref="ClamAvApi"/>.</param>
    public RequestVirusScanMiddleware(ClamAvApi clamAvClient)
    {
        this.clamAvClient = clamAvClient ?? throw new ArgumentNullException(nameof(clamAvClient));
    }

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        if (next == null)
            throw new ArgumentNullException(nameof(next));

        if (!httpContext.Request.HasFormContentType)
        {
            await next(httpContext);

            return;
        }

        foreach (var file in httpContext.Request.Form.Files)
        {
            await using var stream = file.OpenReadStream();
            {
                var result = await this.clamAvClient
                    .Scan(stream);

                var infectedFile = result.InfectedFiles
                    .FirstOrDefault();

                switch (result.Type)
                {
                    case ResultType.Clean:
                        break;

                    case ResultType.Unknown:
                    case ResultType.VirusFound:
                    case ResultType.Error:
                        throw new VirusScanException(result.Type, file.FileName, infectedFile?.VirusName)
                        {
                            Raw = result.Raw
                        };

                    default:
                        throw new ArgumentOutOfRangeException(nameof(result.Type), result.Type, $"Invalid result type: {result.Type}.");
                }
            }
        }

        await next(httpContext);
    }
}