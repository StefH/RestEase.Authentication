using System.Net.Http;

namespace RestEase.Authentication.Authentication;

// ReSharper disable once UnusedTypeParameter
public interface IHttpRequestMessageHandler<T> where T : class
{
    Task AuthenticateHttpRequestMessageAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default);
}