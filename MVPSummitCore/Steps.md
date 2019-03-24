# Graph Core Value Proposition

## Use the GraphClientFactory
Reference Core Nuget (from Local Nugets)

```csharp
  var client = GraphClientFactory.Create("v1.0\",GraphCloud.Global);
  var userResponse = await client.GetStringAsync("me");
 ```
 Request will fail with 401  (and then 400 currently due to baseaddress)

## Create an AuthProvider

Reference Auth Nuget (from Local Nugets)

```csharp
    var app = DeviceCodeProvider.CreateClientApplication("5dba030e-37f3-4adc-8eb8-3e2e9e68aa0f");
    var auth = new DeviceCodeProvider(app, new string[] { "User.Read" });
```

## Integrate the AuthProvider

```csharp
    var handlers = GraphClientFactory.CreateDefaultHandlers().ToList();
    handlers.Insert(0, new AuthenticationHandler(auth));
    var client = GraphClientFactory.Create(handlers: handlers);  
```
Now try it!  
Add the TokenStorageProvider so we don't have to keep doing that dance.  Notice the symmetry with the different Auth Providers.

## Introduce the Logging Handler

```csharp
     handlers.Add(new DemoLoggingHandler());
```

## Introduce the Compression Handler

```csharp
     handlers.Insert(1, new CompressionHandler());
```

