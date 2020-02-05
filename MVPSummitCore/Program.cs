using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

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
            //var app = DeviceCodeProvider.CreateClientApplication("<InsertClientId>", new FileBasedTokenStorageProvider());
            var app = PublicClientApplicationBuilder.Create("<clientId>").Build();
            var auth = new DeviceCodeProvider(app, new string[] { "User.Read" });

            var handlers = GraphClientFactory.CreateDefaultHandlers(auth).ToList();
            handlers.Insert(1, new CompressionHandler());
            handlers.Add(new DemoLoggingHandler());


            var httpClient = GraphClientFactory.Create(handlers, "v1.0/");

            var userResponse = await httpClient.GetStringAsync("me");
            Console.WriteLine();
            Console.WriteLine(userResponse);

            Console.Read();
        }
    }


}
