using System.Net.Http;
using Microsoft.Extensions.Options;
using RestEase.Authentication.Options;
using Stef.Validation;

namespace RestEase.Authentication.Authentication;

public class HeaderAuthenticationHandler<T> : IHttpRequestMessageHandler<T>
    where T : class
{
    private readonly string _headerName;
    private readonly string? _headerValue;

    public HeaderAuthenticationHandler(IOptions<AuthenticatedRestEaseOptions<T>> options)
    {
        var optionsValue = Guard.NotNull(options.Value);

        _headerName = Guard.NotNullOrEmpty(optionsValue.HeaderName);
        _headerValue = optionsValue.HeaderValue;
    }

    public Task AuthenticateHttpRequestMessage(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default)
    {
        httpRequestMessage.Headers.Add(_headerName, _headerValue);
        return Task.CompletedTask;
    }
}