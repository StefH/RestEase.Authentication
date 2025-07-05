using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using RestEase.Authentication.Options;
using Stef.Validation;

namespace RestEase.Authentication.Authentication;

public class BearerAuthenticationHandler<T>(IOptions<AuthenticatedRestEaseOptions<T>> options) : IHttpRequestMessageHandler<T>
    where T : class
{
    private const string Scheme = "Bearer";
    private readonly string _headerValue = Guard.NotNull(options.Value).HeaderValue!;

    public Task AuthenticateHttpRequestMessageAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default)
    {
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(Scheme, _headerValue);
        return Task.CompletedTask;
    }
}