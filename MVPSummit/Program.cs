using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVPSummit
{
    class Program
    {

        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
            Console.Read();
        }

        static async Task AsyncMain()
        {
            // Create Client Application and Authentication Provider

            var app = PublicClientApplicationBuilder.Create("<Insert ClientId here>").Build();
            var authProvider = new InteractiveAuthenticationProvider(app, new string[] { "User.Read"});

            // Create GraphServiceClient with middleware pipeline setup
            var graphServiceClient = new GraphServiceClient(authProvider);

            // Request using default app permissions
            var user = await graphServiceClient.Me.Request().GetAsync();
            Console.WriteLine($"User: {user.DisplayName}");

            // Incremental Consent
            var messages = await graphServiceClient.Me.Messages.Request()
                .WithScopes(new string[] { "Mail.Read" })
                .GetAsync();
            Console.WriteLine($"Messages Count: {messages.Count}");

            Console.Read();
        }
    }
}
