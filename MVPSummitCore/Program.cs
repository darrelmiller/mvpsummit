using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

namespace MVPSummitCore
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
            Console.Read();
        }

        private static async Task AsyncMain()
        {
            var app = DeviceCodeProvider.CreateClientApplication("<InsertClientId>", new FileBasedTokenStorageProvider());
            var auth = new DeviceCodeProvider(app, new string[] { "User.Read" });

            var handlers = GraphClientFactory.CreateDefaultHandlers().ToList();
            handlers.Insert(0, new AuthenticationHandler(auth));
            handlers.Insert(1, new CompressionHandler());
            handlers.Add(new DemoLoggingHandler());


            var httpClient = GraphClientFactory.Create("v1.0/", handlers: handlers);

            var userResponse = await httpClient.GetStringAsync("me");
            Console.WriteLine();
            Console.WriteLine(userResponse);

            Console.Read();
        }
    }


}
