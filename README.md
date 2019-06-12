# Vivet.AspNetCore
A collection of AspNetCore components.  

### Table of Contents
* [Vivet.AspNetCore.RequestTimeZone](#Vivet.AspNetCore.RequestTimeZone)  

***

### Vivet.AspNetCore.RequestTimeZone
[![Build status](https://ci.appveyor.com/api/projects/status/182f7gcym49fc5sq/branch/master?svg=true)](https://ci.appveyor.com/project/vivet/vivet-aspnetcore/branch/master)
[![NuGet](https://img.shields.io/nuget/dt/Vivet.AspNetCore.RequestTimeZone.svg)](https://www.nuget.org/packages/Vivet.AspNetCore.RequestTimeZone/)
[![NuGet](https://img.shields.io/nuget/v/Vivet.AspNetCore.RequestTimeZone.svg)](https://www.nuget.org/packages/Vivet.AspNetCore.RequestTimeZone/)  

Middleware to configure request timezone options.  

The current timezone on a request is set in the Request TimeZone Middleware. The middleware is enabled in the Startup.Configure method, and on every request the list of RequestTimeZoneProvider in the RequestTimeZoneOptions is enumerated and the first provider that can successfully determine the request timezone is used. The default providers come from the RequestTimeZoneOptions class:  

* QueryStringRequestTimeZoneProvider (```?tz=myTimezone```)
* HeaderRequestTimeZoneProvider (```Key: TZ```)
* CookieRequestTimeZoneProvider

The default list goes from most specific to least specific. You can change the order and even add a custom timezone provider, similar to ```Microsoft.AspNetCore.Localization```. If none of the providers can determine the request timezone, the ```DefaultRequestTimeZone``` is used.  

#### Configuration
To configure the Request TimeZone Middleware, first add the required services to the ```IServiceCollection```, as shown below.  
```csharp
services
  .AddRequestTimeZone("myDefaultTimeZone");
```
Next, register the middleware in the pipeline, as shown below.   
```csharp
applicationBuilder
  .UseRequestTimeZone();
```

***

