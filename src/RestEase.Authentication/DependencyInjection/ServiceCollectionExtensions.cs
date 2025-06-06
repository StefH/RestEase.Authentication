using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestEase;
using RestEase.Authentication.Authentication;
using RestEase.Authentication.Http;
using RestEase.Authentication.Options;
using RestEase.Authentication.RetryPolicies;
using RestEase.HttpClientFactory;
using Stef.Validation;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseWithAuthenticatedRestEaseClient<T>(
        this IServiceCollection services,
        IConfigurationSection section,
        Action<RestClient>? configureRestClient = null) where T : class
    {
        Guard.NotNull(services);
        Guard.NotNull(section);

        var options = new AuthenticatedRestEaseOptions<T>();
        section.Bind(options);

        return services.UseWithAuthenticatedRestEaseClient(options, configureRestClient);
    }

    public static IServiceCollection UseWithAuthenticatedRestEaseClient<T>(
        this IServiceCollection services,
        Action<AuthenticatedRestEaseOptions<T>> configureAction,
        Action<RestClient>? configureRestClient = null) where T : class
    {
        Guard.NotNull(services);
        Guard.NotNull(configureAction);

        var options = new AuthenticatedRestEaseOptions<T>();
        configureAction(options);

        return services.UseWithAuthenticatedRestEaseClient(options, configureRestClient);
    }

    public static IServiceCollection UseWithAuthenticatedRestEaseClient<T>(
        this IServiceCollection services,
        AuthenticatedRestEaseOptions<T> options,
        Action<RestClient>? configureRestClient = null) where T : class
    {
        Guard.NotNull(services);
        Guard.NotNull(options);

        if (string.IsNullOrEmpty(options.HttpClientName))
        {
            options.HttpClientName = typeof(T).FullName;
        }

        if (options.LogRequest || options.LogResponse)
        {
            services.AddTransient<CustomHttpLoggingHandler<T>>();
        }

        switch (options.AuthenticationType)
        {
            case AuthenticationType.Bearer:
                services.AddSingleton<IHttpRequestMessageHandler<T>, BearerAuthenticationHandler<T>>();
                break;

            case AuthenticationType.Basic:
                services.AddSingleton<IHttpRequestMessageHandler<T>, BasicAuthenticationHandler<T>>();
                break;

            case AuthenticationType.Header:
                services.AddSingleton<IHttpRequestMessageHandler<T>, HeaderAuthenticationHandler<T>>();
                break;

            default:
                throw new NotSupportedException($"{nameof(AuthenticationType)} '{options.AuthenticationType}' is not supported.");
        }

        // HttpClient and RestEase services
        services
            .AddTransient<AuthenticationHttpMessageHandler<T>>()
            .AddTransient<CustomHttpClientHandler<T>>()
            .AddHttpClient(options.HttpClientName!, httpClient =>
            {
                httpClient.BaseAddress = options.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .ConfigurePrimaryHttpMessageHandler<CustomHttpClientHandler<T>>()
            .AddCustomHttpLoggingHandler(options)
            .AddHttpMessageHandler<AuthenticationHttpMessageHandler<T>>()
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<T>(serviceProvider))
            .UseWithRestEaseClient<T>(config =>
            {
                configureRestClient?.Invoke(config);

                if (config.JsonSerializerSettings != null && options.WriteJsonIndented != null)
                {
                    config.JsonSerializerSettings.Formatting = options.WriteJsonIndented.Value ? Formatting.Indented : Formatting.None;
                }
            });

        services.AddOptionsWithDataAnnotationValidation(options);

        return services;
    }
}