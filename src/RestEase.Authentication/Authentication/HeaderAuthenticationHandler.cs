using System.Net.Http;
using Microsoft.Extensions.Options;
using RestEase.Authentication.Options;
using Stef.Validation;

namespace RestEase.Authentication.Authentication;

public class HeaderAuthenticationHandler<T>(IOptions<AuthenticatedRestEaseOptions<T>> options) : IHttpRequestMessageHandler<T>
    where T : class
{
    private readonly AuthenticatedRestEaseOptions<T> _options = Guard.NotNull(options.Value);

    public Task AuthenticateHttpRequestMessage(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default)
    {
        httpRequestMessage.Headers.Add(_options.HeaderName, _options.Value);
        return Task.CompletedTask;
    }
}