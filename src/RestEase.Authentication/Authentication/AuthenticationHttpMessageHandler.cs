using System.Net.Http;

namespace RestEase.Authentication.Authentication;

internal class AuthenticationHttpMessageHandler<T>(IHttpRequestMessageHandler<T> httpRequestMessageHandler) : DelegatingHandler
    where T : class
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await httpRequestMessageHandler.AuthenticateHttpRequestMessageAsync(request, cancellationToken).ConfigureAwait(false);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}