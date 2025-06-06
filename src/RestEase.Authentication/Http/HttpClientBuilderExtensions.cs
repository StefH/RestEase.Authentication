using Microsoft.Extensions.DependencyInjection;
using RestEase.Authentication.Options;

namespace RestEase.Authentication.Http;

internal static class HttpClientBuilderExtensions
{
    internal static IHttpClientBuilder AddCustomHttpLoggingHandler<T>(this IHttpClientBuilder builder, AuthenticatedRestEaseOptions<T> options) where T : class
    {
        if (options.LogRequest || options.LogResponse)
        {
            return builder.AddHttpMessageHandler<CustomHttpLoggingHandler<T>>();
        }

        return builder;
    }
}