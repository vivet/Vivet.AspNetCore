# Vivet.AspNetCore.RequestVirusScan
[![Build status](https://ci.appveyor.com/api/projects/status/182f7gcym49fc5sq/branch/master?svg=true)](https://ci.appveyor.com/project/vivet/vivet-aspnetcore/branch/master)
[![NuGet](https://img.shields.io/nuget/dt/Vivet.AspNetCore.RequestVirusScan.svg)](https://www.nuget.org/packages/Vivet.AspNetCore.RequestVirusScan/)
[![NuGet](https://img.shields.io/nuget/v/Vivet.AspNetCore.RequestVirusScan.svg)](https://www.nuget.org/packages/Vivet.AspNetCore.RequestVirusScan/)  

Middleware to configure requset virus scan of uploaded files.  

This library uses ClamAV for scanning and detecting virusses and malware in files. A server or container running ClamAV is required to use this middleware.  

***

#### Registration
To configure the Request TimeZone Middleware, first add the required services to the ```IServiceCollection```, as shown below.  
```csharp
services
    .AddRequestVirusScan(x => 
    {
        // Configuration.
    });
```
  
or, read the configuration from the ```clamav``` section in ```app.settings.json```,  
```json
"ClamAv": {
    "Host": "",
    "Port": 0,
    "UseHealthCheck": true
}
```
...and register.
```csharp
services
    .AddRequestVirusScan();
```
  
Next, register the middleware in the pipeline, as shown below.   
```csharp
applicationBuilder
    .UseRequestVirusScan();
```

The middleware is now configured in the pipeline, and all uploaded file will be scaned for virusses.  

***