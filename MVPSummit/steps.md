# It is about time

Auth is painful, what can we do to help.
- Many factors, many scenarios
- MSAL is getting better, more complete, but has to support many scenarios
- Graph is only a subset of those.

## Call using permission defined in App registration

```csharp
    var client = new GraphServiceClient(...);
    var user = await client.Me.Request().GetAsync();
```
We need to provide an implementation of IAuthorizationProvider.  To date we have only provided a class called DelegateAuthorizationProvider with delegated all the work to you.

## Setup an AuthProvider

```csharp
   var app = InteractiveAuthenticationProvider.CreateClientApplication("<InsertClientIdHere>");
   var auth = new InteractiveAuthenticationProvider(app, new string[] { "User.Read" });

    var client = new GraphServiceClient(auth);
```
Run it and see the results

## Make a request using a permission assigned by the app

```csharp
            var events = await graphClient.Me.Request().GetAsync();
            Console.WriteLine($"{events.Count} events");

```

## Incremental Consent

```csharp
            var messages = await graphClient.Me.Messages.Request()
                .WithScopes(new string[] { "Mail.Read"})
                .GetAsync();
            Console.WriteLine($"{messages.Count} messages");
 ```
 Once permissions are in the metadata, then we can do this with our request builders.

## Beta
Reference Beta DLL instead


```csharp
            var identityProviders = await graphClient.IdentityProviders.Request()
                .WithScopes(new string[] { "IdentityProvider.Read.All" })
                .GetAsync();

            Console.WriteLine($"{identityProviders.Count} identity providers");
```

## Where next
AuthProviders are only one component that we are working on.  
You can find more details on our design repo. https://github.com/microsoftgraph/msgraph-sdk-design

## Fluent or not?

```csharp
      var messages2 = await client.CreateRequest<UserMessagesCollectionRequest>("me/messages")
                          .WithScopes(new string[] { "Mail.Read" }).GetAsync();

      Console.WriteLine(messages2.Count);
```


