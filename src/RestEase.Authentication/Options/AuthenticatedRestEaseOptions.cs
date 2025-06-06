using System.ComponentModel.DataAnnotations;

namespace RestEase.Authentication.Options;

// ReSharper disable once UnusedTypeParameter
public class AuthenticatedRestEaseOptions<T> where T : class
{
    [Required]
    public Uri BaseAddress { get; set; } = null!;

    [Required]
    public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.Bearer;

    /// <summary>
    /// Gets or sets the value to use for Bearer or Header.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the value to use for the Header-name.
    /// </summary>
    public string? HeaderName { get; set; }

    /// <summary>
    /// Gets or sets the username for Basic Authentication.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the password for Basic Authentication.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// The HttpClientName to use.
    /// </summary>
    public string? HttpClientName { get; set; }

    /// <summary>
    /// When set to 'true', no validation is done on the HTTPS Certificate when connecting to the 'BaseAddress'.
    /// </summary>
    public bool AcceptAnyServerCertificate { get; set; } = false;

    /// <summary>
    /// This timeout in seconds defines the timeout on the HttpClient which is used to call the 'BaseAddress'.
    /// Default value is 60 seconds.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TimeoutInSeconds { get; set; } = 60;

    /// <summary>
    /// Log the request as Debug Logging.
    /// Default value is <c>false</c>;
    /// </summary>
    public bool LogRequest { get; set; }

    /// <summary>
    /// Log the response as Debug Logging.
    /// Default value is <c>false</c>;
    /// </summary>
    public bool LogResponse { get; set; }

    /// <summary>
    /// Write the request json as indented.
    /// Default value is <c>null</c>;
    /// </summary>
    public bool? WriteJsonIndented { get; set; }
}