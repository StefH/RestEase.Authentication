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
    private readonly string _value = Guard.NotNull(options.Value).Value!;

    public Task AuthenticateHttpRequestMessage(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default)
    {
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(Scheme, _value);
        return Task.CompletedTask;
    }
}