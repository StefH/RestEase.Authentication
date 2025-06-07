## RestEase.Authentication
An extension to [RestEase](https://github.com/canton7/RestEase) which supports Bearer, Basic and ApiKey authentication.

### Usage

#### Example appsettings.json for `Bearer`-Authentication
``` json
{
    "DocumentApiClientOptions": {
        "AuthenticationType": "Bearer",
        "Value": "0418ee68-40b5-44a1-b092-aa26b888a7ca",

        "BaseAddress": "https://localhost:44319"
    }
}
```

#### Example appsettings.json for `Basic`-Authentication
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

#### Example appsettings.json for `Header`-Authentication
``` json
{
    "DocumentApiClientOptions": {
        "AuthenticationType": "Header",
        "HeaderName": "X-API-KEY",
        "Value": "0418ee68-40b5-44a1-b092-aa26b888a7ca",

        "BaseAddress": "https://localhost:44319"
    }
}
```

#### Example C# register DI

``` c#
var section = configuration.GetSection("DocumentApiClientOptions");

services.UseWithAuthenticatedRestEaseClient<IDocumentApi>(section);
```

### 🌐 Links
- See also: [RestEase.Authentication.Azure](https://github.com/StefH/RestEase.Authentication.Azure)