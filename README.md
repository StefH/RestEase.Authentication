![Project Icon](icon.png) RestEase.Authentication
==================================

An extension to [RestEase](https://github.com/canton7/RestEase) which supports Bearer, Basic and ApiKey authentication.

## Install
[![NuGet Badge](https://img.shields.io/nuget/v/RestEase.Authentication)](https://www.nuget.org/packages/RestEase.Authentication)

You can install from NuGet using the following command in the package manager window:

`Install-Package RestEase.Authentication`

Or via the Visual Studio NuGet package manager or if you use the `dotnet` command:

`dotnet add package RestEase.Authentication`

## Usage

### Example appsettings.json for `Bearer`-Authentication
``` json
{
    "DocumentApiClientOptions": {
        "AuthenticationType": "Bearer",
        "HeaderValue": "0418ee68-40b5-44a1-b092-aa26b888a7ca",

        "BaseAddress": "https://localhost:44319"
    }
}
```

### Example appsettings.json for `Basic`-Authentication
``` json
{
    "DocumentApiClientOptions": {
        "AuthenticationType": "Basic",
        "UserName": "u",
        "Password": "p",

        "BaseAddress": "https://localhost:44319"
    }
}
```

### Example appsettings.json for `Header`-Authentication
``` json
{
    "DocumentApiClientOptions": {
        "AuthenticationType": "Header",
        "HeaderName": "X-API-KEY",
        "HeaderValue": "0418ee68-40b5-44a1-b092-aa26b888a7ca",

        "BaseAddress": "https://localhost:44319"
    }
}
```

### Example C# register DI

``` c#
var section = configuration.GetSection("DocumentApiClientOptions");

services.UseWithAuthenticatedRestEaseClient<IDocumentApi>(section);
```

## 🌐 Links
- See also: [RestEase.Authentication.Azure](https://github.com/StefH/RestEase.Authentication.Azure)

## Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=StefH) and [Dapper Plus](https://dapper-plus.net/?utm_source=StefH) are major sponsors and proud to contribute to the development of **RestEase.Authentication**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StefH/resources/main/sponsor/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=StefH)

[![Dapper Plus](https://raw.githubusercontent.com/StefH/resources/main/sponsor/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=StefH)