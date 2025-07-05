using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using RestEase.Authentication.Options;

namespace RestEase.Authentication.Authentication;

public class BasicAuthenticationHandler<T>(IOptions<AuthenticatedRestEaseOptions<T>> options) : IHttpRequestMessageHandler<T>
    where T : class
{
    private const string Scheme = "Basic";
    private readonly Lazy<string> _basicAuthentication = new(() => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{options.Value.Username}:{options.Value.Password}")));

    public Task AuthenticateHttpRequestMessageAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default)
    {
        httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue(Scheme, _basicAuthentication.Value);
        return Task.CompletedTask;
    }
}