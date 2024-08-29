# Vivet.AspNetCore.RequestTimeZone
[![Build status](https://ci.appveyor.com/api/projects/status/182f7gcym49fc5sq/branch/master?svg=true)](https://ci.appveyor.com/project/vivet/vivet-aspnetcore/branch/master)
[![NuGet](https://img.shields.io/nuget/dt/Vivet.AspNetCore.RequestTimeZone.svg)](https://www.nuget.org/packages/Vivet.AspNetCore.RequestTimeZone/)
[![NuGet](https://img.shields.io/nuget/v/Vivet.AspNetCore.RequestTimeZone.svg)](https://www.nuget.org/packages/Vivet.AspNetCore.RequestTimeZone/)  

Middleware to configure request timezone options.  

The current timezone on a request is set in the Request TimeZone Middleware. The middleware is enabled in the Startup.Configure method, and on every request the list of providers configured in the ```RequestTimeZoneOptions``` is enumerated, and the first provider in order, that can successfully determine the request timezone is applied. The default providers are:  

* QueryStringRequestTimeZoneProvider (```?tz=myTimezone```)
* CookieRequestTimeZoneProvider
* HeaderRequestTimeZoneProvider (```tz=myTimezone```)

The default list goes from most specific to least specific. You can change the order and even add a custom timezone provider, similar to the implementation of ```Microsoft.AspNetCore.Localization```. If none of the providers can determine the request timezone, the ```DefaultRequestTimeZone``` is used.  

#### Registration
To configure the Request TimeZone Middleware, first add the required services to the ```IServiceCollection```, as shown below.  
```csharp
services
    .AddRequestTimeZone("myDefaultTimeZone");
```
Or,
```csharp
services
    .AddRequestTimeZone(x => 
    {
        // Configuration.
    });
```

Next, register the middleware in the pipeline, as shown below.   
```csharp
applicationBuilder
    .UseRequestTimeZone();
```

The middleware is now configured in the pipeline, and will register request timezone when suplied by one of the configured providers.  
When a request model contains properties of type ```DateTimeOffset```, those will be converted to utc datetime, based on the request timezone. Subsequently, when a response is serialized, the ```DateTimeOffset``` properties are converted back to local datetime.  

#### Accessors
When a timezone is provided as part of a request, it can be retrieved through the ```IRequestTimeZoneFeature``` from a controller, as follows:
```csharp
var requestTimeZone = this.HttpContext.Features
    .Get<tRequestTimeZoneFeature>()
    .RequestTimeZone;
```

Alternatively, the feature may be accessed through the extension method ```GetUserTimeZone()``` of ```HttpContext```, as shown below:
```csharp
var requestTimeZone = httpContext.GetUserTimeZone();
```

Finally, the library also contains a ```ThreadStatic``` accessor, called ```DateTimeInfo```.  
The implementation exposes the ```RequestTimezone```, as well an utc and local datetime that is based on the timezone. 

```csharp
var now = DateTimeInfo.Now  // Gets datetime
var utc = DateTimeInfo.UtcNow  // Gets utc datetime
var timezone = DateTimeInfo.TimeZone.Value  // Gets the request timezone.
```

***
