using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestEase.Authentication.Options;

namespace RestEase.Authentication.Http;

/// <summary>
/// This custom HttpClientHandler can override the ServerCertificateCustomValidationCallback.
/// </summary>
internal class CustomHttpClientHandler<T> : HttpClientHandler where T : class
{
    public CustomHttpClientHandler(IOptions<AuthenticatedRestEaseOptions<T>> options, ILogger<CustomHttpClientHandler<T>> logger)
    {
        if (options.Value.AcceptAnyServerCertificate)
        {
            logger.LogInformation("AcceptAnyServerCertificate is set to {true}, this means that no validation is done on the HTTPS Certificate when connecting to '{BaseAddress}'.", true, options.Value.BaseAddress);
            ServerCertificateCustomValidationCallback += (_, _, _, _) => true; // (message, cert, chain, errors)
        }
    }
}